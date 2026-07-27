# Horror Memory - Prompt Batch

Questo file contiene prompt pronti per generare:
- Background `Original` e `Distorted`
- 15 coppie differenze (`started` / `distorted`) per livello

Convenzioni:
- Stile coerente con `LevelsDesign.md`
- Composizione FLAT, no profondita
- Batch one-by-one: L2 completato, L3 in corso

---

## Livello 2 - Il Corridoio (Facile: 6 attive)

### A) Background

**L2 - Original Background**
`Frontal flat view of a home entryway wall, orthographic composition, no perspective depth. Wooden console table, ceramic lamp, snake plant in pot, key tray, grouped family photo frames, coat hooks with tote bag and scarf, oval mirror, closed white door with brass handle, HOME doormat on wooden floor. Cozy modern-rustic Italian home. Warm neutral palette, clean and lived-in details. Painted illustration style, slightly stylized, not photorealistic. 16:9, 1920x1080.`

**L2 - Distorted Background**
`Same exact composition and object placement as the original background. Wall aged and partially peeling, subtle mold stains, light cobwebs in corners, colder blue-green lighting, faint grime on floor edges, slight decay mood but readable details. Psychological horror atmosphere, no gore. Painted illustration style, slightly stylized, not photorealistic. 16:9, 1920x1080.`

---

### B) Differenze (15 slot)

Note produzione:
- Genera ogni sprite su sfondo trasparente (`transparent background`)
- Mantieni scala compatibile con UI (small/medium prop cutout)
- Ogni coppia ha `Started` (normale) e `Distorted` (horror)

#### Slot 01 - Pianta (attiva)
**Started:** `Small potted healthy snake plant, clean ceramic pot, transparent background, painted game asset, front view.`
**Distorted:** `Same potted plant but wilted blackened leaves, subtle decay, transparent background, painted game asset, front view.`

#### Slot 02 - Lampada (attiva)
**Started:** `Table lamp turned on, warm bulb glow, beige lampshade, transparent background, painted game asset, front view.`
**Distorted:** `Same table lamp turned off, cracked lampshade and slight dark stain, transparent background, painted game asset, front view.`

#### Slot 03 - Chiavi + biglietto (attiva)
**Started:** `Small key set on a round tray, transparent background, painted game asset, top-front angle.`
**Distorted:** `Same key set on tray plus a folded note reading "Ricordi?", transparent background, painted game asset, top-front angle.`

#### Slot 04 - Cornici foto extra (medio+)
**Started:** `Cluster of 8 simple family photo frames, neutral faces, transparent background, painted game asset, front view.`
**Distorted:** `Cluster of 9 family photo frames, one extra child portrait added, transparent background, painted game asset, front view.`

#### Slot 05 - Specchio presenza (attiva)
**Started:** `Oval wall mirror with neutral reflection blur, transparent background, painted game asset, front view.`
**Distorted:** `Same oval mirror with faint monstrous silhouette reflection (Death misread as monster), transparent background, painted game asset, front view.`

#### Slot 06 - Porta (attiva)
**Started:** `White wooden door fully closed, brass handle, transparent background, painted game asset, front view.`
**Distorted:** `Same white wooden door slightly ajar with dark gap, brass handle, transparent background, painted game asset, front view.`

#### Slot 07 - Tappetino (difficile+)
**Started:** `Rectangular doormat text HOME, clean texture, transparent background, painted game asset, top view.`
**Distorted:** `Same HOME doormat stained and worn, transparent background, painted game asset, top view.`

#### Slot 08 - Borsa con foglio (medio+)
**Started:** `Beige canvas tote bag closed, transparent background, painted game asset, hanging front view.`
**Distorted:** `Same tote bag slightly open with paper sheet visible, transparent background, painted game asset, hanging front view.`

#### Slot 09 - Sciarpa (difficile+)
**Started:** `Knitted scarf folded naturally, transparent background, painted game asset, hanging front view.`
**Distorted:** `Same scarf hanging in an unnaturally elongated hair-like shape, transparent background, painted game asset, hanging front view.`

#### Slot 10 - Lampadario (medio+)
**Started:** `Simple ceiling lamp fixture static, transparent background, painted game asset, front view.`
**Distorted:** `Same ceiling lamp fixture angled as if oscillating, transparent background, painted game asset, front view.`

#### Slot 11 - Macchia pavimento (medio+)
**Started:** `Clean wooden floor patch, transparent background, painted game asset, top view tile cutout.`
**Distorted:** `Same floor patch with dark irregular stain, transparent background, painted game asset, top view tile cutout.`

#### Slot 12 - Foto volto cancellato (attiva)
**Started:** `Family photo frame with smiling faces visible, transparent background, painted game asset, front view.`
**Distorted:** `Same family photo frame, one face erased/scratched out, transparent background, painted game asset, front view.`

#### Slot 13 - Ragnatela (medio+)
**Started:** `Empty wall corner patch clean, transparent background, painted game asset, corner cutout.`
**Distorted:** `Same corner patch with large cobweb, transparent background, painted game asset, corner cutout.`

#### Slot 14 - Riflesso occhi (difficile+)
**Started:** `Mirror reflection neutral blur only, transparent background, painted game asset, front view.`
**Distorted:** `Mirror reflection with two faint eyes emerging from darkness, transparent background, painted game asset, front view.`

#### Slot 15 - Graffio battiscopa (difficile+)
**Started:** `Baseboard clean segment, transparent background, painted game asset, front view strip.`
**Distorted:** `Same baseboard segment with long claw-like scratch, transparent background, painted game asset, front view strip.`

---

### C) Attivazione suggerita per difficolta

- **Facile (6):** 01, 02, 03, 05, 06, 12
- **Medio (10):** Facile + 04, 08, 11, 13
- **Difficile (14):** Medio + 07, 09, 14, 15

---

## Livello 3 — Il Negozio (esterno) (Facile: 6 attive)

**Trama:** Segui la fattura trovata nel PC. Il negozio è chiuso di notte. Dietro la tapparella qualcosa si muove — ti sembra un mostro.

### Come funziona nel gioco (importante)

Nel codice:
- Sotto: `Distorted` background
- Sopra: `Original` background (svanisce col timer)
- Overlay: le differenze (`startedSprite` → al click diventa `distortedSprite`)

Quindi:

| Cosa | Regola |
|------|--------|
| **Background** | Scena base. Original e Distorted devono avere **gli stessi oggetti nelle stesse posizioni**. Distorted cambia SOLO atmosfera (luce teal, texture, mood). |
| **Diff sprite** | Solo gli oggetti che il giocatore deve trovare. **Non duplicare** lampione/tapparella/insegna già disegnati nel BG. |
| **Mai** | Generare un secondo lampione se il lampione è già nel BG. Mai inventare citofoni, muretti, telecamere, saracinesche blu neon. |

### Verdetto asset attuali

| Asset | Verdetto | Perché |
|-------|----------|--------|
| `Original_3.png` | **Tenere** (base buona, flat) | Vetrina frontale ok |
| `Disturbed_3.png` | **Rifare in parte** | Ha già cotto differenze (crepe, AIUTO, sagoma, lettere rotte). Con il fade del timer quelle roba appare da sola senza click. Distorted deve restare **stessa scena** di Original, solo tinta teal/fredda. |
| `Diff_03.png` | **Rifare tutto** | Saracinesche blu, stile neon, oggetti inventati (muretto, citofono, telecamera) — non centrano |

### Pipeline corretta da ora in poi

1. **Original BG** = scena normale completa  
2. **Distorted BG** = **stessa composizione pixel-per-pixel**, solo mood horror (teal, più scuro). **Niente** crepe/AIUTO/sagoma/lettere rotte cotte dentro  
3. **15 Diff** = overlay che **non esistono** (o coprono una zona “neutra”) nel BG. Ogni slot = oggetto piccolo da trovare, started + distorted

Esempi sensati di Diff per QUESTA scena (oggetti aggiunti / sostituiti, non doppioni del BG):

| # | Diff (overlay) | Started | Distorted | Attiva |
|---|----------------|---------|-----------|--------|
| 1 | Cartello appeso vetrina sx | `CHIUSO` rosso | `AIUTO` | ✓ |
| 2 | Foglie/carta in vetrina | Nessuna / vuota | Scritta a mano sul vetro | ✓ |
| 3 | Sagoma dietro vetro sx | Vuoto / solo elettrodomestico sfocato | Figura alta (mostro) | ✓ |
| 4 | Lucchetto sulla tapparella | Lucchetto chiuso | Lucchetto aperto | |
| 5 | Biglietto su ciottoli | Assente | Biglietto bianco a terra | ✓ |
| 6 | Cartello orario (overlay sul vetro dx) | `Aperto domani ore 9:00` | `Non aprire mai` | |
| 7 | Crepa sul vetro sx | Vetro integro (patch trasparente) | Crepa a ragno | |
| 8 | Crepa sul vetro dx | Vetro integro | Crepa a ragno | |
| 9 | Lettera dell'insegna (patch) | Lettera integra | Lettera incrinata | ✓ |
| 10 | Stella in più nel cielo | Assente | Stella / bagliore | |
| 11 | Cavo che pende in vetrina | Cavo normale | Cavo a forma di mano | |
| 12 | Macchia sui ciottoli | Assente | Macchia scura | |
| 13 | Occhi nella sagoma / riflesso | Assente | Due occhi nella vetrina | ✓ |
| 14 | Sottotitolo patch | `RIPARAZIONI` | `RICORDI` | |
| 15 | Ragnatela angolo insegna | Assente | Ragnatela | |

> Nota: cartelli `CHIUSO` / `Aperto...` se sono **già dipinti** nel BG Original, o li togli dal BG e li metti solo come Diff, oppure il Diff è un patch che **copre esattamente** quella zona. Non disegnare un secondo cartello diverso altrove.

### Prompt Distorted BG (rifare)

```
Use Original_3.png as strict reference. Keep EXACT same composition, objects, positions, signs, shutters, lantern, moon, cobblestones.
ONLY change: cold teal/blue-green color grade, darker mood, slight grime.
Do NOT add: cracked glass, AIUTO sign, silhouette figure, broken letters, new objects.
Same flat frontal shop facade. Painted illustration style. 16:9.
```

### Prompt Diff sheet (rifare)

```
Generate 15 difference pairs as OVERLAY sprites for a spot-the-difference game.
Reference: Original_3.png (attached).

RULES:
- Sprites go ON TOP of the background. Do not redraw the whole lantern, whole shutters, or whole shop.
- Each pair = small prop/cutout that can be placed on the scene (sign, padlock, crack patch, silhouette, note on ground, etc.)
- Started = normal version. Distorted = horror version.
- Match the painted style and warm palette of Original_3 for Started; teal/cold for Distorted.
- Transparent background.
- FORBIDDEN: blue neon shutters, modern glass buildings, intercoms, security cameras, clawed brick walls, anything not fitting this Italian stone shop facade.
```

### C) Attivazione (Facile — 6)

- **Attive:** 01, 02, 03, 05, 09, 13
- **Riserva:** 04, 06, 07, 08, 10, 11, 12, 14, 15

