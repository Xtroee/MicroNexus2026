# Desain Karakter Antibiotik — Versi 3 (Pivot Gaya: Sci-Fi Mascot)

**Keputusan**: ganti arah gaya visual dari "Soldier tier" lama (armor cel-shaded simpel, gak pegang senjata) ke
gaya baru yang lebih detail/polished — "sci-fi mascot dengan prop/senjata khas". Berlaku ke **semua 14 karakter**
(7 lama + 7 baru), bukan cuma yang baru. Warna identitas tiap karakter TETEP SAMA kayak yang udah ada (biar gak
ilang branding) — yang berubah cuma gaya render & nambahin prop/senjata yang mereka pegang.

Direverse-engineer dari 2 contoh yang kamu suka & udah generate sendiri (prompt aslinya lupa).

> **Update metode (penting)**: cara paling akurat BUKAN prompt teks panjang kayak di bagian 3 di bawah — itu
> ketauan pas nyoba lampirin **beberapa gambar karakter referensi sekaligus** (3-4 gambar, campur yang lama &
> yang baru) + instruksi pendek doang ("make a new variety of character based on these reference images, match
> the style, [detail warna/prop/tanpa-senjata sesuai tabel]"). Modelnya jauh lebih akurat niru gaya dari banyak
> contoh visual sekaligus daripada dari deskripsi teks panjang. **Prompt di bagian 3 tetep kepake** buat isi
> detail (warna, prop, motif) yang perlu disebutin, tapi cara paling manjur: lampirin gambar dulu, baru
> teks-nya dipersingkat jadi instruksi + detail spesifik doang (gak perlu re-describe seluruh gaya dari nol).

> **Update juga**: gak semua karakter pegang senjata/prop — di-mix, liat kolom "Prop/senjata" di tabel bawah,
> yang ditandai "—" berarti desainnya cukup dari kepala/hood/creature motif doang (kayak contoh slime-hood),
> gak perlu ada yang dipegang.

---

## 1. Style DNA baru (dari 2 contoh)

Yang KONSISTEN di kedua contoh (jadi wajib ada di semua 14):
- Proporsi chibi (kepala/elemen ekspresif besar dibanding badan)
- **Mata kuning/emas menyala** = satu-satunya fitur ekspresif yang keliatan (tetep dipertahankan dari versi lama)
- **Pegang/pakai 1 prop/senjata khas** yang merepresentasikan cara kerja obatnya di dunia nyata (ini elemen BARU,
  versi lama gak ada yang pegang senjata)
- Utility belt dengan pouch kecil
- 1 elemen "mengalir" ke satu sisi (cape ATAU ekor asap/energi — boleh pilih sesuai tema karakter)
- Rendering cel-shaded glossy dengan highlight tajam warna-senada, outline gelap tebal
- Background solid flat 1 warna

Yang BOLEH BEDA per karakter (ruang kreatif, gak perlu seragam):
- Badan: bisa full-armor berdiri kayak knight (kontoh 1), ATAU makhluk/kepala unik yang ngambang tanpa kaki
  (contoh 2) — pilih yang paling pas sama "kepribadian" obatnya.
- Bentuk kepala/wajah: helm, hood, atau bentuk non-humanoid (awan, slime, dll).

---

## 2. Tabel 14 karakter (warna tetep sama, prop & badan baru)

| # | Antibiotik | Warna (tetep) | Prop/senjata | Badan | Kenapa |
|---|---|---|---|---|---|
| 1 | Amoxicillin | White | — (gak pegang apa-apa) | Hood/creature motif | Obat dasar/paling umum — cukup simpel, gak perlu prop |
| 2 | Vancomisin | Blue | Tombak/staff berbentuk selang infus (IV drip) | Berdiri, armor | Vancomycin cuma dikasih lewat infus IV — prop nguatin cerita |
| 3 | Cefixime | Pink | — (gak pegang apa-apa) | Hood/creature motif | Sefalosporin oral ringan, gak perlu prop |
| 4 | Azitromisin | Red | — (gak pegang apa-apa) | Hood/creature motif | Cukup kuat dari desain kepala/motif "3 hari" doang |
| 5 | Ciprofloxacin | Green | — (gak pegang apa-apa, tetep hood elf-ear) | Hood/creature motif | Udah punya identitas kuat dari hood-nya |
| 6 | Levofloksasin | Yellow | — (sibling Cipro, ikut gak pegang) | Hood/creature motif | Konsisten sama Cipro (satu keluarga fluorokuinolon) |
| 7 | Cotrimoxazole | Black | Twin dagger/dual blade | Berdiri, armor | Kombinasi 2 obat sekaligus — prop nguatin cerita |
| 8 | Meropenem | Bronze/Copper | Cannon/blaster besar dengan tangki infus glowing | Berdiri, armor ringan | IV, broad-spectrum powerhouse — "big gun" |
| 9 | Ceftriaxone | Purple | Lance/tombak bentuk syringe raksasa | Berdiri, armor | Obat suntik (IM/IV) — prop nguatin cerita |
| 10 | Linezolid | Cyan | Rapier energi tipis | Ngambang/kepala unik | Obat sintetis presisi tinggi — prop nguatin cerita |
| 11 | Clarithromycin | Coral-Orange | — (sibling Azitromisin, ikut gak pegang) | Hood/creature motif | Konsisten sama Azitromisin (satu keluarga makrolid) |
| 12 | Penicillin V | Deep Brown/Sepia | Pedang klasik + shield (ksatria tua) | Berdiri, armor | "Yang paling OG" — prop paling penting buat narasinya |
| 13 | Colistin | Olive/Khaki | Blaster bocor/berkarat dengan cairan toksik menetes | Ngambang/kepala unik | Last-resort berbahaya — prop nguatin kesan toksik |
| 14 | Doxycycline | Teal-Green | — (gak pegang apa-apa) | Hood/creature motif | Modern/sleek cukup dari desain kepala doang |

---

## 3. Prompt siap-pakai (14 karakter)

Format: DNA bersama + variabel per karakter, ditulis lengkap biar langsung bisa di-paste.

### 1. Amoxicillin (White)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp white/silver
highlight edges and thick dark outline. Standing armored knight build. Glowing amber eyes are the only visible
facial feature, seen through a face-covering scarf/mask under a helmet. White as the dominant color with gold
trim accents. Holding a short sword shaped like a capsule/pill (rounded double-tone blade, representing the
most basic first-line oral antibiotic). White cape flows to one side. Utility belt with small pouches. Confident
standing pose. Solid flat colored background (not white, not transparent), no text, no watermark.
```

### 2. Vancomisin (Blue)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp blue highlight
edges and thick dark outline. Standing armored knight build. Glowing amber eyes are the only visible facial
feature, seen through a face-covering scarf/mask under a helmet. Blue as the dominant color with silver trim
accents. Holding a tall staff/spear shaped like an IV drip line and bag (a glowing fluid-filled bag at the top,
a tube winding down the shaft), representing that this drug is only given by IV infusion. Blue cape flows to one
side. Utility belt with small pouches. Confident standing pose. Solid flat colored background (not blue), no
text, no watermark.
```

### 3. Cefixime (Pink)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp pink highlight
edges and thick dark outline. Standing armored knight build. Glowing amber eyes are the only visible facial
feature, seen through a face-covering scarf/mask under a helmet. Pink as the dominant color with gold trim
accents. Holding a small quick dagger shaped like a pill/capsule, held in a fast ready-to-strike grip
(representing a light, fast-acting oral antibiotic). Pink cape flows to one side. Utility belt with small
pouches. Confident standing pose. Solid flat colored background (not pink), no text, no watermark.
```

### 4. Azitromisin (Red)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp red highlight
edges and thick dark outline. Floating/hovering build with no visible legs — the body fades into a wispy
trailing red energy tail below, like a spirit. The head is a distinctive non-humanoid shape (glowing red-orange
wispy cloud or flame-like form) with glowing amber eyes as the only facial feature. Red as the dominant color on
the flight-suit/torso with dark trim accents. Holding a sci-fi blaster gun with a small glowing cartridge
labeled with "3" markings (representing the classic 3-day "Z-pack" course). Utility belt with small pouches.
Aiming the blaster forward, dynamic hovering pose. Solid flat colored background (not red), no text, no
watermark.
```

### 5. Ciprofloxacin (Green)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp green highlight
edges and thick dark outline. Standing armored knight build. Glowing amber eyes are the only visible facial
feature, seen through a face-covering scarf/hood with pointed elf-like tips. Green as the dominant color with
silver trim accents. Holding a glowing green energy shield in one hand and a short sword in the other
(representing broad-spectrum coverage — ready to defend and attack in any direction). Green cape flows to one
side. Utility belt with small pouches. Confident standing pose. Solid flat colored background (not green), no
text, no watermark.
```

### 6. Levofloksasin (Yellow)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp yellow highlight
edges and thick dark outline. Standing armored knight build, visually a taller/more evolved sibling of a green
fluoroquinolone-family ranger. Glowing amber eyes are the only visible facial feature, seen through a
face-covering scarf/hood with pointed elf-like tips. Yellow as the dominant color with silver trim accents.
Holding a long two-handed sword (longer and more refined than a basic sword, representing a third-generation
upgrade). Yellow cape flows to one side. Utility belt with small pouches. Confident standing pose. Solid flat
colored background (not yellow), no text, no watermark.
```

### 7. Cotrimoxazole (Black)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp
purple-tinted-black highlight edges and thick dark outline. Standing armored knight build. Glowing amber eyes
are the only visible facial feature, seen through a face-covering scarf/mask under a helmet. Black as the
dominant color with gold trim accents. Holding two matching short daggers, one in each hand (representing this
being a combination of two different drugs working together — trimethoprim and sulfamethoxazole). Black cape
flows to one side. Utility belt with small pouches. Confident dual-wielding stance. Solid flat colored
background (not black), no text, no watermark.
```

### 8. Meropenem (Bronze/Copper)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp bronze/copper
highlight edges and thick dark outline. Standing build with light, sleek, agile-fitted armor (not bulky).
Glowing amber eyes are the only visible facial feature, seen through a narrow visor slit on a full enclosed
helmet (no brim). Bronze/copper metallic as the dominant color with dark navy trim. Holding a large cannon-like
blaster with a glowing IV-fluid tank attached to the side via a tube (representing this being the strongest,
broad-spectrum IV powerhouse drug, reserved for the most severe infections). A scarf flows to one side. Utility
belt with small pouches. Confident standing pose. Solid flat colored background, no text, no watermark.
```

### 9. Ceftriaxone (Purple)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp violet/purple
highlight edges and thick dark outline. Standing armored knight build. Glowing amber eyes are the only visible
facial feature, seen through a face-covering scarf/mask under a helmet. Deep violet/purple as the dominant color
with gold trim accents. Holding a tall lance/spear with a large syringe-shaped tip (a glowing needle point,
representing that this drug is given as a single injectable dose). Purple cape flows to one side. Utility belt
with small pouches. Confident standing pose, lance held ready to thrust. Solid flat colored background, no text,
no watermark.
```

### 10. Linezolid (Cyan)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp cyan highlight
edges and thick dark outline. Floating/hovering build with no visible legs — body fades into a wispy trailing
cyan energy tail. The head is a distinctive non-humanoid glowing cyan crystalline/geometric shape, with glowing
pale-blue eyes as the only facial feature (representing a fully lab-synthesized, precision-engineered drug, not
derived from nature). Cyan as the dominant color on the torso with white/silver trim accents. Holding a thin
glowing energy rapier (a precise, surgical weapon). Utility belt with small pouches. Elegant poised stance,
hovering. Solid flat colored background, no text, no watermark.
```

### 11. Clarithromycin (Coral-Orange)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp coral-orange
highlight edges and thick dark outline. Floating/hovering build with no visible legs, body fading into a wispy
trailing orange energy tail — visually a sibling design to a red-colored macrolide-family ranger, but in
coral-orange. The head is a distinctive glowing orange wispy cloud/flame-like non-humanoid shape, glowing amber
eyes as the only facial feature. Coral-orange as the dominant color on the flight-suit with dark trim accents.
Holding a sci-fi blaster gun similar in shape to a sibling character's, with a small glowing cartridge. Utility
belt with small pouches. Aiming the blaster forward, dynamic hovering pose. Solid flat colored background, no
text, no watermark.
```

### 12. Penicillin V (Deep Brown/Sepia)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp warm brown/bronze
highlight edges and thick dark outline. Standing armored knight build with a rounded, old-fashioned domed helmet
(not a modern visor cap) — an "elder / original" look, distinct from the more sci-fi-styled rangers. Glowing
warm amber eyes are the only visible facial feature, seen through a face-covering scarf. Deep sepia brown /
aged-leather as the dominant color with antique brass trim accents. Holding a classic simple sword and a round
shield (a traditional knight's weapon set, representing this being the very first antibiotic ever discovered).
Brown cape flows to one side. Utility belt with small pouches. Confident, dignified standing pose. Solid flat
colored background, no text, no watermark.
```

### 13. Colistin (Olive/Khaki)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp sickly
yellow-green highlight edges and thick dark outline. Floating/hovering build with no visible legs, body fading
into a wispy trailing toxic-green vapor tail. The head is a distinctive weathered, cracked, non-humanoid
toxic-looking shape (like corroded metal or a decaying gas mask), glowing pale-yellow eyes as the only facial
feature. Muted olive-khaki as the dominant color on the worn, scuffed flight-suit with cracked hazard-yellow
warning stripe accents. Holding a rusty, leaking blaster gun with a cracked glowing toxic-green canister
dripping fluid, wrapped in warning tape (representing a toxic, dangerous, last-resort drug). Utility belt with
small pouches. Grim, weary but determined hovering pose. Solid flat colored background, no text, no watermark.
```

### 14. Doxycycline (Teal-Green)
```
Chibi mascot character, polished cel-shaded video game sprite art, glossy rendering with sharp deep teal-green
highlight edges and thick dark outline. Floating/hovering build with no visible legs, body fading into a wispy
trailing seafoam-green energy tail. The head is a sleek, modern glowing teal-green geometric non-humanoid shape
with a subtle capsule/pill-shell pattern etched into it, glowing pale-green eyes as the only facial feature
(representing a modern, updated-generation drug). Deep teal-green as the dominant color on the torso with silver
trim accents. Holding a sleek modern blaster pistol (cleaner, more refined design than older-generation
weapons). Utility belt with small pouches. Confident, poised hovering pose. Solid flat colored background, no
text, no watermark.
```

---

## 3.5. Update workflow: base canvas + edit, bukan generate ulang tiap kali

> **SUPERSEDED (lihat 3.6)**: pendekatan "1 base axolotl-hoodie, edit warna/aksesoris per karakter" di bawah ini
> udah gak dipake lagi buat karakter tersisa — ternyata cuma reskin, gak beneran diverse. Diganti sama pendekatan
> "konsep makhluk beda total per karakter" (bagian 3.6). Bagian ini disimpen sebagai riwayat/pelajaran proses,
> dan sebagian tabelnya (warna, prop, icon) masih valid dipakai.

Setelah beberapa iterasi, ketauan cara paling reliable itu BUKAN nulis prompt-generate-baru tiap karakter (hasil
tiap kali beda, gampang drift), tapi: **ambil 1 gambar yang udah bagus sebagai base**, terus minta AI **edit**
gambar itu (ganti warna, tambah/ganti 1-2 elemen) buat tiap karakter — bukan generate dari nol.

**Base yang dipakai**: gambar axolotl chibi (hoodie abu-abu, tactical vest item badge+pouch, sarung tangan,
celana cargo, sepatu boots warna-warni, beanie dengan patch bulat kecil, ekor). Proporsi & rendering-nya udah
pas, tinggal di-edit per karakter.

**Template instruksi edit** (pola yang dipake buat semua 14):
```
Edit this exact image. Keep the same pose, proportions, outfit style (hoodie, casual cross-body sling bag with
1-2 small pouches, gloves, cargo pants, sneaker-boots), and rendering style exactly as-is — do not regenerate
from scratch. Change only:
- Creature/skin color → [WARNA]. Make the skin coloring organic and alive, NOT flat/robotic — soft gradient
  shading, subtle warm undertone variation, light blush/highlight on the cheeks.
- Head accessory → [HEAD ACCESSORY, bukan beanie tiap kali — variasiin]. Icon/detail kecil di situ → [ICON]
- Face/mask → [PAKE MASKER atau ENGGAK, variasiin]. Kalau pake: scarf/gaiter nutupin idung-mulut, mata doang
  yang keliatan. Kalau enggak: full face keliatan, boleh ubah bentuk/gaya mata dikit (jangan selalu mata bulat
  amber identik) biar tiap karakter kerasa beda.
- [kalau ada prop] Add [PROP] held in one hand
Keep everything else in the image identical to the reference. Keep the overall vibe casual/streetwear, NOT
tactical/military — no ID badge.
```

> **Gotcha ketemu pas Meropenem**: hasil pertama kepalanya keliatan "botak"/kosong karena scarf penutup muka
> kelewat gak ke-generate. Udah dimasukin ke template (walau sekarang jadi opsional, liat gotcha berikutnya).
>
> **Gotcha ketemu pas Ceftriaxone**: (1) beanie di semua karakter jadi ngebosenin/template-y — sekarang
> divariasiin per karakter (kolom "Head accessory"). (2) warna kulit flat/uniform bikin kesan robot, bukan
> makhluk hidup — sekarang wajib pakai gradasi organik. (3) masker+mata identik di semua karakter juga
> ngebosenin — sekarang opsional & bentuk mata boleh beda per karakter (kolom "Face/mata").

**Tabel edit delta per karakter** (warna & prop sama kayak yang udah diputusin di bagian 2):

| # | Antibiotik | Warna | Head accessory | Face/mata | Appendage kepala (gill/sirip) | Icon/detail | Prop tambahan |
|---|---|---|---|---|---|---|---|
| 1 | Amoxicillin | White | Beanie polos | Masker, mata standar | Default (gill axolotl) | Kapsul/pil simpel | — |
| 2 | Vancomisin | Blue | Beanie polos | Masker, mata standar | Default | Tetesan infus (IV drop) | Staff/tombak bentuk selang infus |
| 3 | Cefixime | Pink | Beanie polos | Masker, mata standar | Default | Pil | — |
| 4 | Azitromisin | Red | Beanie polos | Masker, mata standar | Default | Angka "3" / jam (kursus 3 hari) | — |
| 5 | Ciprofloxacin | Green | Beanie polos | Masker, mata standar | Default | Kompas/perisai (broad-spectrum) | — |
| 6 | Levofloksasin | Yellow | Beanie polos | Masker, mata standar | Default | Panah ke atas (evolusi dari Cipro) | — |
| 7 | Cotrimoxazole | Black | Beanie polos | Masker, mata standar | Default | 2 lingkaran overlap (kombinasi 2 obat) | Twin dagger |
| 8 | Meropenem | Bronze/Copper | Beanie (selesai) | Masker (selesai) | Default (selesai) | Perisai/benteng | Cannon/blaster + tangki infus |
| 9 | Ceftriaxone | Purple | Cap (selesai, hasil akhir) | Full face (selesai, hasil akhir) | Default/gill-wing (selesai) | Syringe | Lance bentuk syringe raksasa |
| 10 | Linezolid | Cyan | Tech visor/goggle didorong ke dahi | Full face, mata besar bulat "innocent" | **Antena tipis dengan bola kecil menyala di ujung** (tema sintetis/tech) | Circuit/lab flask | Rapier energi |
| 11 | Clarithromycin | Coral-Orange | Bandana ala bajak laut | Full face, mata ceria melengkung | **Tentakel anemon bergelombang lembut** (cocok sama warna coral) | Sama keluarga icon Azitromisin | — |
| 12 | Penicillin V | Deep Brown/Sepia | Tanpa penutup kepala | Full face, mata tua/bijaksana | **Polos, tanpa appendage** (kesan sederhana/klasik/"yang asli") | Mahkota/laurel | Pedang + shield klasik |
| 13 | Colistin | Olive/Khaki | Hood usang di belakang | Masker (gas-mask vibe), mata lelah/sayu | **Layu/terkulai, kayak busuk/rusak** (tema toksik) | Hazard/tengkorak kecil | Blaster bocor cairan toksik |
| 14 | Doxycycline | Teal-Green | Beanie (selesai) | Masker (selesai) | Default (selesai) | Panah upgrade | — |

Catatan: kolom-kolom ini ke-7 lama (Amoxicillin s/d Cotrimoxazole) masih default karena belum diprioritasin
(lihat catatan prioritas di atas) — kalau nanti giliran dikerjain, boleh divariasiin juga.

**Contoh siap-pakai (Amoxicillin, karakter #1)**:
```
Edit this exact image. Keep the same pose, proportions, outfit style (hoodie, casual cross-body sling bag with
1-2 small pouches, gloves, cargo pants, sneaker-boots, beanie), and rendering style exactly as-is — do not
regenerate from scratch. Change only:
- Creature/skin color → white/off-white
- Beanie patch icon → a simple pill/capsule icon
- Add a face-covering scarf/gaiter around the neck that wraps up over the nose and mouth, leaving only the eyes
  visible. White/off-white tone, slightly darker shade for contrast/folds.
Keep everything else in the image identical to the reference. Keep the vibe casual/streetwear, not
tactical/military — no ID badge.
```

Tinggal ganti bagian warna/icon/prop sesuai baris di tabel buat 13 sisanya. Kalau pakai gambar hasil edit
sebelumnya (bukan axolotl asli) sebagai base buat karakter berikutnya, itu juga boleh — asal masih dari 1 "garis
keturunan" gambar yang sama biar konsistensinya kejaga (bukan campur reference dari sumber yang beda-beda lagi).

> **Update base template**: axolotl di atas udah di-upgrade ke **badan polosan netral** (gak ada gill/fin/ekor,
> gak ada ciri hewan spesifik) — ini base resmi yang dipake sekarang, gantiin axolotl. Pertanyaan ekor di atas
> otomatis kejawab: gak ada ekor dari awal di template baru ini.

**Progress tracking**:

> **Update prioritas**: 7 yang LAMA (Amoxicillin s/d Cotrimoxazole) udah punya art (biar beda gaya) — biarin dulu,
> gak usah di-redo sekarang. Fokus selesain 7 yang BARU dulu (belum punya art sama sekali). Bagian "temuan 7 lama
> gak konsisten" di bagian 4 tetep valid sebagai catatan, cuma dieksekusinya belakangan.

**7 BARU (prioritas sekarang)**:

| # | Antibiotik | Status |
|---|---|---|
| 8 | Meropenem | ❌ **belum** — percobaan kumbang badak DITOLAK (user kecewa), nunggu klarifikasi kenapa sebelum coba lagi |
| 9 | Ceftriaxone | ✅ selesai (masih gaya axolotl-hoodie, sebelum pivot 3.6) |
| 10 | Linezolid | ✅ selesai (masih gaya axolotl-hoodie, sebelum pivot 3.6) |
| 11 | Clarithromycin | ✅ selesai (masih gaya axolotl-hoodie, sebelum pivot 3.6) |
| 12 | Penicillin V | belum — pakai template axolotl-hoodie, lihat tabel di bagian 3.5 (kepala polos, mata tua/bijaksana, pedang+shield klasik) |
| 13 | Colistin | belum — pakai template axolotl-hoodie, lihat tabel di bagian 3.5 (hood usang, masker gas-mask, blaster bocor toksik) |
| 14 | Doxycycline | ✅ selesai (masih gaya axolotl-hoodie, sebelum pivot 3.6) |

**7 LAMA (belakangan)**: Amoxicillin, Vancomisin, Cefixime, Azitromisin, Ciprofloxacin, Levofloksasin,
Cotrimoxazole — semua masih pakai art lama yang udah ada (`Assets/Art/Capsule Character/Soldier/*`), belum
disentuh.

---

## 3.6. REVERTED — konsep makhluk beda total per karakter (ditolak user)

> **Status: ditolak, jangan dipakai.** Ide di bagian ini (tiap karakter jadi konsep makhluk beda total: kumbang,
> teddy bear, tong beracun) awalnya kelihatan bagus (Meropenem-beetle dikira works), tapi begitu dibandingin
> LANGSUNG sama karakter lain di roster yang sama (Ceftriaxone/Linezolid/Clarithromycin/Doxycycline, semua dari
> template axolotl-hoodie yang sama), hasilnya malah "jomplang" — kerasa gak satu keluarga/roster. Konsistensi
> visual antar-karakter ternyata lebih penting daripada variasi konsep ekstrem per karakter.
>
> **Balik ke arah bagian 3.5**: semua karakter (termasuk Meropenem) tetep dari template axolotl-hoodie yang
> sama, variasi cukup dari warna/head-accessory/appendage/prop/face (kolom-kolom di tabel 3.5) — BUKAN ganti
> konsep badan/spesies dari nol tiap karakter. Sisa bagian 3.6 di bawah ini disimpen sebagai riwayat aja, gak
> dieksekusi.

Ngeliat semua hasil dikumpulin bareng (termasuk 7 karakter lama yang beda gaya), ketauan: variasi warna/aksesoris
di 1 badan yang sama (axolotl-hoodie) TETEP kerasa "itu-itu aja". Yang bikin roster lama kerasa unik itu justru
karena tiap karakter konsepnya beda total (robot, anak kostum dino, anak streetwear, astronot) — bukan cuma beda
kostum. ~~(Kesimpulan ini salah — lihat status di atas: setelah dicoba beneran, hasilnya malah kebalikannya:
merusak konsistensi roster.)~~

**Aturan baru buat sisa karakter**: ~~tiap karakter = konsep makhluk/badan yang beda total~~ **DIBATALKAN**.

**Meropenem (DITOLAK)**: percobaan kumbang badak lapis baja — bagus secara individual, tapi jomplang dibanding
roster. Direvisi balik ke axolotl-hoodie template (lihat prompt terbaru di chat).

**Konsep sisa 2 karakter (JANGAN DIPAKAI, disimpen sebagai riwayat)**:
- **Penicillin V** (Sepia Brown) — teddy bear tua yang idup: badan boneka beruang usang berwarna coklat sepia,
  jahitan kasar keliatan di beberapa sambungan, kancing sebagai salah satu mata (mata satunya bercahaya normal),
  telinga beruang bundar. Pegang pedang kayu mainan + tameng kayu kecil (representasi "yang paling original/
  klasik", kesan hangat-nostalgia bukan sci-fi).
- **Colistin** (Olive/Khaki) — tong beracun berjalan: badan drum/barrel logam karatan warna olive-khaki, ada
  tanda hazard pudar di badannya, bocor cairan toksik hijau menetes dari sambungan yang udah berkarat, kaki-tangan
  pipa/selang improvisasi, wajah/mata mengintip dari celah tutup drum yang udah penyok. Pegang blaster bocor yang
  udah dibuat sebelumnya. Kesan berbahaya, udah tua, "last resort".

---

## 4. Temuan penting: 7 karakter lama TERNYATA udah gak konsisten dari awal

Dicek langsung ke semua folder `Assets/Art/Capsule Character/Soldier/*` — bukan cuma White/Red/Green yang jadi
acuan gaya "ranger fantasi" (cape+armor+scarf) selama ini. 4 karakter lain ternyata beda gaya total:

- **Blue (Vancomisin)** — robot/mecha sci-fi (helm metal penuh, shield teknologi bercahaya, drone kecil terbang
  di sampingnya)
- **YellowOrange (Levofloksasin)** — anak pakai kostum kigurumi dinosaurus kuning-hitam, wajah ungu ngintip dari
  hood
- **Pink New (Cefixime)** — anak gaya streetwear, headphone, tas ransel putih-pink
- **Black (Cotrimoxazole)** — astronot ungu-hitam gelap, ada patch bendera Indonesia di dada

Jadi "gaya versi 1" yang selama ini jadi acuan (White/Red/Green) itu sebenernya cuma 3 dari 7 — bukan
representasi keseluruhan roster. Ini artinya pivot ke gaya baru (bagian 1-3 di atas) bukan cuma buat nyeragamin
7 baru + nyamain ke 3 lama, tapi realistisnya PERLU nyeragamin ke-14 dari nol, termasuk redo total buat
Blue/YellowOrange/Pink/Black yang sekarang malah lebih jauh dari target gaya baru dibanding White/Red/Green.

---

## 5. Catatan implementasi

- Warna semua 14 **tetep sama** kayak yang udah ditentuin sebelumnya (7 lama = branding existing, 7 baru = dari
  revisi warna kemarin) — cuma gaya render, prop, dan (buat sebagian) badan yang baru.
- 7 lama (Amoxicillin s/d Cotrimoxazole) butuh di-generate ULANG dari nol dalam gaya baru ini — art lama
  (`Assets/Art/Capsule Character/Soldier/*`) jadi outdated kalau pivot ini jadi dipakai penuh.
- Belum nyentuh kode/aset project — ini masih tahap bikin gambar. Kalau art-nya udah jadi, kasih tau buat lanjut
  ke tahap import & wiring ke Unity.
