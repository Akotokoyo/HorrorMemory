# Horror Memory - Prompt Batch

Questo file contiene prompt pronti per generare:
- Background `Original` e `Distorted`
- 15 coppie differenze (`started` / `distorted`) per livello

Convenzioni:
- Stile coerente con `LevelsDesign.md`
- Composizione FLAT, no profondita
- Livello 2 in questo primo batch (one-by-one)

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

