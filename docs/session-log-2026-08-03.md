# MicroNexus — Session Log (2026-08-03)

Skripsi: tower defense tentang resistensi antimikroba. Tugas: tambah 6 karakter, 6 stage, 6 mikroba, + rapikan UI yang gak konsisten (~1 bulan).

Log ini nyimpen context kerjaan Claude Code sesi ini biar bisa di-pickup lagi nanti tanpa harus re-explain dari nol.

---

## 1. Level Map (Candy Crush-style path selector)

Redesign total dari stage selector lama jadi peta jalur berkelok ala Candy Crush Saga, tema "perjalanan di dalam tubuh manusia" (permintaan dosen pembimbing).

### File baru
- `Assets/Scripts/Level/LevelNodeView.cs` — kontrol 1 node level: `NodeState` enum (`Locked`/`Unlocked`/`Completed`), `Setup()`/`SetState()`.
- `Assets/Scripts/Level/LevelMapController.cs` — controller utama. Struct `LevelNodeData` (posisi, state, scene, display name) dan `StagePathStyle` (jarak antar node, amplitudo swing, dll per stage). Method utama: `RefreshMap()`, `BuildNodeData()`, `ComputeNodePosition()` (zigzag horizontal pakai Sin + Perlin noise), `DrawPathLine()` (titik-titik, bukan garis solid).
- `Assets/Prefab/LevelMap/LevelNode.prefab` — node bulat (65% ukuran asli, ~91px), children: `Image_Base`, `Image Icon` (alpha 0), `Text_LevelNumber`, `Image_DimOverlay`, `Image_LockOverlay`, `Image_CompleteBadge`, `Text_LevelName`.
- `Assets/Prefab/LevelMap/LineSegment.prefab` — dot kecil buat garis penghubung titik-titik.
- `Assets/Art/Placeholder/circle_placeholder.png`, `space_cell_background.png` — background & sprite node dibuat prosedural (bukan aset gambar asli, digenerate via `Texture2D.SetPixel`).
- `Assets/Scenes/Stage Selection/Level Map.unity` — scene baru, Canvas ScaleWithScreenSize (800x600 ref), ScrollRect horizontal.
- `Assets/Scenes/MainMenu/Mode Selection.unity` — scene baru buat cabang "Level Biasa" vs "Level Endless".

### Alur navigasi final
`MainMenu → Mulai Bermain → Mode Selection → (Level Biasa → Level Map) | (Level Endless → Level Endless)`

Pause menu di semua scene Level 1-6 Stage 1 diarahkan ke "Level Map" (sebelumnya "Stage 1 Selection").

`Assets/Scenes/Stage Selection/_Deprecated/New Stage Selector.unity` — scene lama, dipindah ke folder deprecated (orphaned, gak dipakai lagi).

### Bug yang pernah ketemu & fix-nya
- **Node prefab offset ke luar layar**: anchor prefab salah `(0.5,0.5)` (center) harusnya `(0,0.5)` (left-origin, sesuai konvensi container). Diagnosis lewat compare `anchoredPosition` vs `localPosition`.
- **Zigzag Y collapse jadi konstan**: `viewport.rect.height` masih 0 saat `Start()` karena ScrollRect belum sempat layout. Fix: `Canvas.ForceUpdateCanvases()` + `LayoutRebuilder.ForceRebuildLayoutImmediate()` di awal `RefreshMap()`.
- **Node kotak bukan bulat**: child `Image Icon` & root hit-area image punya warna putih opaque tanpa sprite → keliatan kotak putih solid. Fix: alpha=0.
- **EventSystem dobel**: ada `EventSystem` component nyantol langsung di `Canvas` GameObject (harusnya cuma di GameObject `EventSystem` sendiri) → break input di Play mode. Dihapus.

---

## 2. Bacteria damage-effectiveness feedback + hover name

Data damage-tier per-warna udah ada dari dulu di tiap `*Projectile.cs` (0=immune, 1-19=partial, 20=full-effective) tapi gak pernah ditampilin ke player. Sesi ini bikin itu keliatan.

### File baru (di `Assets/Scripts/Bacteria Controller/`)
- `BacteriaLabelFactory.cs` — factory static bikin label (background panel gelap + TextMeshPro), dipakai bareng oleh popup damage & label hover nama.
- `BacteriaEffectivenessPopup.cs` — popup melayang "Effective"/"Less Effective"/"Immune" berdasar damage value (threshold 20 = full effective).
- `FloatingPopupText.cs` — animasi float-up + fade-out selama 1 detik, lalu self-destroy.

### Modifikasi (identik di 7 file `BacteriaController{Blue,Pink,Orange,Red,Green,Yellow,Purple}.cs`)
- Field `speciesName` (nama latin spesies masing-masing).
- `OnMouseEnter`/`OnMouseExit` → nampilin nama spesies on-hover (lazy-created child label).
- `ProjectileHit(int damage)` manggil `BacteriaEffectivenessPopup.Show()` sebelum health dikurangi.

### Iterasi visibility (per feedback user)
1. Awalnya outline TMP doang → kurang keliatan, terutama "Less Effective".
2. Ganti ke background panel solid (hitam 75% alpha) di belakang teks, auto-size dari `textBounds`.
3. Terakhir: ukuran diperkecil (fontSize popup 3→2.2, hover 2.5→1.8) sesuai request "kecilin dikit lagi aja".

Species ↔ controller mapping:

| Prefab | Controller | Nama Latin |
|---|---|---|
| Esherchia Coli | `BacteriaControllerPink` | Escherichia coli |
| Haemophilus influenzae | `BacteriaControllerBlue` | Haemophilus influenzae |
| MRSA | `BacteriaControllerOrange` | Methicillin-Resistant Staphylococcus aureus |
| Moracella xataralis | `BacteriaControllerRed` | Moraxella catarrhalis |
| Pseudomonas aeruginosa | `BacteriaControllerGreen` | Pseudomonas aeruginosa |
| Staphylloccocus aureus | `BacteriaControllerYellow` | Staphylococcus aureus |
| Streptococcus pneumonia | `BacteriaControllerPurple` | Streptococcus pneumoniae |

---

## 3. Lock stage dimatikan sementara + badge centang hijau

Permintaan: matiin fitur lock stage sampai ada aba-aba dari dosen pembimbing; level yang udah clear dikasih placeholder centang hijau.

- **[LevelMapController.cs](../Assets/Scripts/Level/LevelMapController.cs) baris ~116**: logic `NodeState` disederhanain jadi cuma `Completed` vs `Unlocked` — gak ada lagi `Locked`. Semua level bisa diklik terlepas dari progress. **Gampang di-revert**: tinggal kembaliin baris `: NodeState.Locked` (ada komentar penjelas di kode persis di titik itu).
- Machinery locking (`enum NodeState.Locked`, `lockOverlay`, `dimOverlay`) **sengaja dibiarin utuh** di `LevelNodeView.cs` — cuma jalur kodenya yang di-bypass, jadi nyalain lagi tinggal ubah 1 baris.
- **Checkmark hijau**: dicari dulu ke seluruh `Assets/Art` (30+ subfolder) — gak ada aset centang. Digenerate prosedural via Unity `Texture2D.SetPixel` (bulatan hijau + outline navy + centang putih, gaya konsisten sama `Green Flag.png`/`Padlock.png` yang udah ada) → `Assets/Art/Placeholder/checkmark_green.png`. Di-wire ke child `Image_CompleteBadge` pada prefab `LevelNode.prefab`, diposisikan jadi badge kecil (30x30) di pojok kanan-atas node (anchor `(1,1)`).

Verifikasi dilakuin lewat query state langsung di kode (bukan screenshot — lihat catatan gotcha di bawah), semua 6 node level konsisten: level completed → badge aktif + `interactable=True`; level belum sampai → badge nonaktif tapi tetap `interactable=True` (lock lepas).

---

## 4. Graphify (knowledge graph project)

Project ini pakai `/graphify` buat bikin knowledge graph dari codebase (`graphify-out/graph.json`, `manifest.json`, dll di root project).

### Bug yang ketemu & fix
Percobaan `--update` awalnya salah scope — nge-detect 2074 file "berubah" (termasuk ~1900 gambar) padahal cuma ~15 file yang beneran disentuh sesi ini. Root cause: graph awal cuma nyakup `Assets/Scripts` (126 file) tapi update dijalanin dengan scope lebih luas, bikin semua path relatif geser dan kelihatan "berubah semua".

**Fix**: scope ulang manual ke `Assets/Scripts` (root yang bener), ekstrak AST ulang cuma buat 12 file kode yang beneran diubah sesi ini (7 BacteriaController + `BacteriaEffectivenessPopup`/`BacteriaLabelFactory`/`FloatingPopupText` + `LevelMapController`/`LevelNodeView`), merge ke graph lama, re-cluster, regenerate report/html — tanpa LLM, tanpa nyentuh ribuan gambar. Sempat ada bug sampingan (source_file path prefix gak konsisten antar file baru vs lama) — udah dinormalisasi.

Hasil akhir: `graph.json` 1591 node / 2353 edge / 104 komunitas, 0 pending changes.

**Belum di-update lagi buat perubahan lock/checkmark di sesi ini** (LevelMapController.cs berubah lagi setelah update terakhir) — kalau mau di-refresh, tinggal minta `/graphify --update` lagi, sekarang polanya udah dipahami jadi harusnya lancar.

---

## 5. UI overhaul (belum jalan — pending)

User kasih referensi CC0 asset pack: https://playpug.itch.io/simple-vector-ui-pack — pilihan style "Depth" + "Border" (alasan: sprite UI existing gak konsisten dan masih ada bekas background eraser).

**Status**: nunggu user download ZIP-nya sendiri (Claude gak bisa download/beli file per safety rules) dan kasih tau lokasi filenya biar bisa di-extract, di-setup 9-slice import settings-nya, terus mulai ganti tombol-tombol existing satu-satu.

---

## Gotcha teknis penting (biar gak keulang)

- **Unity MCP screenshot tool sering stuck return frame stale/cache** — kadang byte-identical persis meski game state udah beda-beda, bahkan setelah full Stop→Play cycle. Kalau ini kejadian lagi: jangan buang waktu di situ, verifikasi lewat `execute_code` query state langsung (component values, bukan pixel).
- **`Destroy()` di Unity itu deferred** — kalau manggil `RefreshMap()` manual lewat reflection buat testing terus langsung query ulang di call terpisah, node lama yang harusnya udah di-destroy masih suka "hidup" karena editor gak nge-tick frame antar tool call MCP. Fix cepat: `DestroyImmediate()` manual buat objek yang gak ada di tracked list, jangan andelin `Destroy()` biasa pas testing via automation.
- **Jangan jalanin automated testing (execute_code rapid-fire/Play-Stop cycling) bersamaan waktu user lagi testing manual di Editor yang sama** — pernah kejadian user lapor "gak bisa deploy soldier" gara-gara Claude lagi rapid-test di instance Unity yang sama. Kalau user lapor ada yang aneh pas lagi kerja bareng: stop dulu automated testing, baru diagnosis.
- **Path project ada spasi** (`E:\SKRIPSI BACKUPP\MicroNexus`) — beberapa tool/script kadang sensitif soal ini, perhatiin quoting.
