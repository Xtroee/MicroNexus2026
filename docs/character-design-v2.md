# Desain Karakter Antibiotik — Versi 2 (7 karakter baru)

"Versi 1" = 7 Ranger yang udah ada (Amoxicillin/White, Vancomisin/Blue, Cefixime/Pink, Azitromisin/Red,
Ciprofloxacin/Green, Levofloksasin/Yellow, Cotrimoxazole/Black). "Versi 2" = 7 karakter baru dari roadmap
Stage 7-13 ([antibiotic-bacteria-matrix-v2.md](antibiotic-bacteria-matrix-v2.md)) untuk Meropenem, Ceftriaxone,
Linezolid, Clarithromycin, Penicillin V, Colistin, Doxycycline.

---

## 1. "DNA" gaya visual versi 1 (dari referensi langsung, bukan tebakan)

Dicek langsung dari `Assets/Art/Capsule Character/Soldier/{Red,White,Green}/Idle/frame_00...gif`:

- **Proporsi chibi/semi-realistis**: badan pendek-gempal, kepala besar, tapi rendering-nya gak kartun-flat — ada
  cel-shading anime dengan highlight tajam & gradasi warna halus.
- **Wajah tertutup**: mata (kuning/emas, bersinar) adalah satu-satunya fitur wajah yang kelihatan — sisanya
  ketutup scarf/masker yang nutupin hidung-mulut, dan topi/hood yang nutupin dahi.
- **Kepala**: ada 2 varian yang udah dipake — topi baseball-cap dengan visor datar (Red, White), atau hood
  longgar dengan ujung runcing kayak telinga elf (Green). Keduanya sah, boleh pilih salah satu per karakter baru.
- **Scarf/cape**: selalu ada, ngalir ke satu sisi (kiri), warnanya senada sama tema karakter.
- **Armor**: chestplate segmented, shoulder pad bulat, sarung tangan bulat menggenggam (fist), boots berat. Trim
  armor bisa solid-color (Red/White pake gradasi warna sendiri) ATAU metallic silver/gold terpisah (Green).
- **Belt**: selalu ada belt coklat/gelap dengan pouch persegi kuning-emas kecil-kecil.
- **Pose**: berdiri gagah, satu tangan agak terangkat (siap tempur), bayangan tipis di bawah kaki.
- **Rendering**: transparent background, glossy highlight di helm/armor, garis outline gelap tegas (ala game
  mobile/gacha character art, bukan flat vector).
- **Palet**: SATU warna dominan per karakter (gradasi terang-gelap dari warna itu doang) + aksen emas/perunggu di
  detail kecil (buckle, trim). Warna yang UDAH DIPAKE: White, Blue, Pink, Red, Green, Yellow, Black — 7 karakter
  baru harus pake warna lain biar gak ketuker.

---

## 2. 7 konsep karakter baru

| # | Antibiotik | Warna baru | Motif pembeda |
|---|---|---|---|
| 1 | Meropenem | ~~Silver~~ → **Bronze/Copper metalik** | ~~Armor tebal/tank~~ → **light armor, gesit** — helm penuh (bukan topi baseball) + bandolier. Broad-spectrum = "elite commando" cepat-tanggap, bukan tank lambat |
| 2 | Ceftriaxone | **Deep Violet/Purple** | Motif syringe/vial kecil di pinggang (obat suntik, beda dari Cefixime yang oral) |
| 3 | Linezolid | **Cyan/Electric Teal** | Armor paling ramping & ringan — obat sintetis murni, kesan "presisi/lab" |
| 4 | Clarithromycin | ~~Amber/Marigold Gold~~ → **Coral-Orange** | Bentuk scarf mirip Azitromisin (sama-sama makrolid), beda warna aja — "saudara" (digeser dari Amber biar gak nabrak Bronze-nya Meropenem) |
| 5 | Penicillin V | ~~Ivory/Parchment~~ → **Deep Brown/Sepia + kuningan** | Helm bulat model klasik (bukan visor sci-fi) — kesan "yang paling tua/OG" |
| 6 | Colistin | ~~Charcoal/Abu gelap~~ → **Olive/Khaki kusam + retakan kuning hazard** | Armor terlihat usang/berkarat, motif garis peringatan kuning-hitam (toksik) |
| 7 | Doxycycline | **Deep Teal-Green/Seafoam** | Motif pola kapsul/cangkang di lempengan armor — kesan modern/generasi baru |

> **Revisi warna (setelah nyoba generate Meropenem)**: 3 warna awal (Silver, Ivory, Charcoal) ternyata kedeketan
> sama warna yang UDAH ada (White, White, Black) — seberapa pun beda aksesoris fisiknya, dua karakter yang warna
> dasarnya mirip tetep bakal keliatan "kembar" sekilas. Diganti ke warna yang jelas beda hue-nya, bukan cuma beda
> saturasi/kegelapan.

---

## 3. Prompt generative image AI (siap pakai)

Format prompt disamain kayak formula versi 1, tinggal ganti bagian warna & motif. Ditulis bahasa Inggris (model
image-gen umumnya lebih akurat dibaca prompt Inggris). Semua diakhiri instruksi **solid plain background**
(bukan "transparent") — banyak model image-gen ngasih artefak aneh (garis checkered kebawa, edge gak bersih)
kalau diminta transparent langsung. Solid background jauh lebih gampang dibersihin manual pake tool remove-bg
(kayak yang keliatan dari nama file `-removebg-preview` yang udah ada di project ini) abis di-generate.

**Cara pakai paling akurat**: jangan cuma pake prompt teks doang — lampirin salah satu gambar karakter versi 1
yang UDAH ada (contoh: `Assets/Art/Capsule Character/Soldier/White/Idle/frame_00_delay-0.1s.gif`) sebagai
reference image bareng prompt di bawah, terus minta model niru gaya visualnya PERSIS (outline, cel-shading,
proporsi, bentuk scarf) — cuma ganti warna & motif. Jauh lebih akurat daripada ngandelin deskripsi teks doang.

**Gotcha yang ketemu pas nyoba**: kalau cuma disuruh "recolor + gaya sama", modelnya (Gemini) cenderung ngasilin
karakter yang identik banget sama reference-nya (cuma beda warna doang) — instruksi abstrak kayak "make the
armor thicker/bulkier" gampang diabaikan. Fix-nya: kasih **atribut fisik konkret** yang gak ada di reference
(pauldron berlapis, bandolier/strap diagonal, gauntlet lebih tebal, stance lebih lebar, dst.) — bukan instruksi
abstrak soal ukuran/proporsi. 4 kandidat atribut yang kepake buat Meropenem (bisa dipakein juga ke karakter lain
kalau kena masalah sama):
- Double-layered pauldron (shoulder armor berlapis, lebih besar & angular dari reference)
- Bandolier strap diagonal di dada dengan 2-3 canister pouch kecil
- Forearm gauntlet/vambrace yang lebih tebal & panjang (nutup sampe lewat pergelangan tangan)
- Stance lebih lebar (kaki lebih renggang, badan lebih lebar dibanding kepala)

### Meropenem
```
Chibi armored ranger character, video game sprite art style, cel-shaded anime rendering with sharp glossy
highlights. Bronze / copper metallic color scheme (warm reddish-brown metal, NOT silver or gray) with dark navy
trim and brass accent buckles. Light, sleek, form-fitting armor plating — the slimmest, most agile build in the
roster (like an elite fast-response commando, not a heavy tank).

Headwear: a full enclosed combat helmet (NOT a baseball cap) — smooth rounded dome shape with a narrow
horizontal visor slit, through which only glowing amber eyes are visible. No brim/bill on this helmet, unlike
the reference's cap.

A face-covering scarf/wrap still flows from the neck to the left side like a cape, same as the reference. Add a
diagonal bandolier strap crossing the chest with 2-3 small cylindrical canister pouches attached (this stays
even though the armor is light — it's gear, not bulk). Slim, form-fitting forearm gauntlets/vambraces (sleek,
not thick). Rounded shoulder pads (single layer, not stacked). Gloved clenched fists, low-profile boots. Dynamic,
forward-leaning agile stance (weight on the balls of the feet, ready to dash), standing on a subtle soft shadow.
Thick dark outline, solid plain flat-color background (any single flat color clearly different from the
character's own palette, NOT transparent), no text.
```

### Ceftriaxone
```
Chibi armored ranger character, video game sprite art style, cel-shaded anime rendering with sharp glossy
highlights. Deep violet / royal purple color scheme with dark trim and gold accent buckles. Sleek segmented
armor with a small syringe-and-vial emblem detail on the hip pouch (representing an injectable medicine).
Baseball-cap-style helmet with flat visor, only glowing amber eyes visible above a face-covering scarf flowing
to the left side like a cape. Rounded shoulder pads, gloved clenched fists, armored boots. Confident
battle-ready stance, standing on a subtle soft shadow. Thick dark outline, solid plain flat-color background (any single flat color clearly different from the character's own palette, NOT transparent), no text.
```

### Linezolid
```
Chibi armored ranger character, video game sprite art style, cel-shaded anime rendering with sharp glossy
highlights. Bright cyan / electric teal color scheme with white and silver trim. Slim, lightweight, streamlined
armor plating (leanest build in the roster) with subtle glowing circuit-line patterns etched into the chest
plate (representing a fully synthetic lab-made drug). Hood-style headwear with pointed elf-like tips, only
glowing pale-blue eyes visible above a face-covering scarf flowing to the left side. Rounded shoulder pads,
gloved clenched fists, sleek boots. Confident battle-ready stance, standing on a subtle soft shadow. Thick dark
outline, solid plain flat-color background (any single flat color clearly different from the character's own
palette, NOT transparent), no text.
```

### Clarithromycin
```
Chibi armored ranger character, video game sprite art style, cel-shaded anime rendering with sharp glossy
highlights. Bright coral-orange color scheme (clearly orange, not gold/amber and not red) with dark trim and
bronze accents. Segmented armor silhouette visually similar in shape to a red-and-black "azithromycin" sibling
ranger, but recolored coral-orange to show they're the same drug family (macrolides). Baseball-cap-style helmet
with flat visor, only glowing amber eyes visible above a face-covering scarf flowing to the left side like a
cape. Rounded shoulder pads, gloved clenched fists, armored boots. Confident battle-ready stance, standing on a
subtle soft shadow. Thick dark outline, solid plain flat-color background (any single flat color clearly
different from the character's own palette, NOT transparent), no text.
```

### Penicillin V
```
Chibi armored ranger character, video game sprite art style, cel-shaded anime rendering with sharp glossy
highlights. Deep sepia brown / aged leather color scheme (a clearly saturated warm brown, NOT ivory, cream, or
white) with antique brass and dark trim (aged, classic look, distinct from the other more sci-fi-styled
rangers). Rounded, old-fashioned domed helmet (not a flat-visor cap) giving an "elder / original knight" feel,
since this is the very first antibiotic ever discovered. Only glowing warm amber eyes visible above a
face-covering scarf flowing to the left side like a cape. Rounded shoulder pads, gloved clenched fists, sturdy
boots. Confident, dignified battle-ready stance, standing on a subtle soft shadow. Thick dark outline, solid
plain flat-color background (any single flat color clearly different from the character's own palette, NOT
transparent), no text.
```

### Colistin
```
Chibi armored ranger character, video game sprite art style, cel-shaded anime rendering with sharp glossy
highlights. Muted olive-khaki color scheme (a dull, desaturated yellow-green — NOT charcoal, NOT gray, NOT
black), armor deliberately weathered and worn with visible cracks and scuffs, accented with cracked
hazard-yellow warning stripes (representing toxicity — a dangerous last-resort drug). Baseball-cap-style helmet
with flat visor, only glowing pale-yellow eyes visible above a face-covering scarf flowing to the left side like
a cape. Rounded shoulder pads, gloved clenched fists, heavy scuffed boots. Grim, weary but determined
battle-ready stance, standing on a subtle soft shadow. Thick dark outline, solid plain flat-color background
(any single flat color clearly different from the character's own palette, NOT transparent), no text.
```

### Doxycycline
```
Chibi armored ranger character, video game sprite art style, cel-shaded anime rendering with sharp glossy
highlights. Deep teal-green / seafoam color scheme with silver trim and bronze accent buckles. Sleek modern
armor plating with a subtle capsule/pill-shell pattern embossed into the chest plate. Hood-style headwear with
pointed elf-like tips, only glowing pale-green eyes visible above a face-covering scarf flowing to the left side.
Rounded shoulder pads, gloved clenched fists, armored boots. Confident battle-ready stance, standing on a subtle
soft shadow. Thick dark outline, solid plain flat-color background (any single flat color clearly different from the character's own palette, NOT transparent), no text.
```

---

## 4. Soal skill image-gen

Aku cek — gak ada skill image-generation yang bisa langsung aku pasang buat kamu (tool `generate_image` yang ada
di Unity MCP butuh API key fal.ai/OpenRouter yang belum di-setup, dan sekarang Unity MCP-nya lagi disconnect
juga). Prompt di atas tinggal kamu paste langsung ke tool AI generatif yang biasa kamu pake — dari nama file yang
udah ada di folder Art (`ChatGPT_Image_Nov_27...png`, `Flux_Schnell_...png`) kelihatannya kamu udah pernah pake
ChatGPT image gen & Flux, jadi prompt di atas format-nya emang disesuaiin buat itu.
