# Horror Memory — Design Livelli 2–19

> Documento di riferimento per trama, composizione immagini e 15 differenze per livello.  
> Livelli 0–1 completati. Questo file copre i **18 livelli restanti** (indice codice 2–19).

---

## Indice

1. [Trama e arco narrativo](#1-trama-e-arco-narrativo)
2. [Regole composizione FLAT](#2-regole-composizione-flat)
3. [Sistema delle 15 differenze](#3-sistema-delle-15-differenze)
4. [Riepilogo livelli](#4-riepilogo-livelli)
5. [Dettaglio livello per livello](#5-dettaglio-livello-per-livello)
6. [Asset esistenti: tenere o rigenerare](#6-asset-esistenti-tenere-o-rigenerare)
7. [Prompt style guide (AI)](#7-prompt-style-guide-ai)
8. [Domande aperte](#8-domande-aperte)

---

## 1. Trama e arco narrativo

### Sinossi

Stai rivivendo un ricordo — quello in cui **avevi preso quel PC**, poco prima di morire. Non lo capisci ancora. Credi di essere a casa, di esplorare, di indagare. Lo schermo del PC mostra immagini della tua vita con dettagli che non tornano.

Ai margini compare una **figura**. All'inizio ti sembra un mostro — qualcosa di sbagliato, di minaccioso. Non sai che è **la Morte**. È venuta a prenderti, e tu stai attraversando l'ultimo ricordo prima di accettarlo.

**Tema:** la memoria è una bugia che ci raccontiamo per sopravvivere — finché non resta più tempo per mentire.

### Premessa (lore)

| Elemento | Significato |
|----------|-------------|
| **Il PC** | Oggetto che il protagonista aveva preso prima di morire. Il ricordo ruota attorno a questo gesto. |
| **Le immagini** | Frammenti del ricordo distorti — ogni differenza è una crepa nella versione che la mente costruisce per ritardare l'inevitabile. |
| **La Figura** | La Morte. All'inizio appare come mostro; solo alla fine il protagonista (e il giocatore) capiscono chi è. |
| **La casa (Liv. 0)** | Casa unica del protagonista. L'asset attuale (Toscana) potrebbe essere errato — da rigenerare o correggere. Il liv. 19 torna allo **stesso ingresso**. |
| **Il giocatore** | Vive il ricordo in prima persona, senza capire di essere già morto. |

### Arco in 3 atti (livelli 0–19)

| Atto | Livelli (codice) | Tema |
|------|------------------|------|
| **I — L'Intruso** | 0–5 | PC in casa, indagine sul negozio, disagio crescente |
| **II — I Ricordi** | 6–13 | Passato: infanzia, partenza, ospedale, funerale |
| **III — La Verità** | 14–19 | Presente e passato si fondono, accettazione |

### Filo narrativo unificato

Unisce `Story.md` e il filo investigativo già in `it.json` (fattura nel PC → negozio):

| Livello | Titolo | Collegamento narrativo |
|---------|--------|------------------------|
| 0 | Il Pacco | Pacco sulla soglia, PC dentro |
| 1 | La Stanza del PC | PC acceso, prima immagine strana, fattura trovata |
| 2 | Il Corridoio | Il PC mostra il corridoio di casa — ombre sbagliate |
| 3 | Il Negozio (esterno) | Segui la fattura, negozio PC chiuso di notte |
| 4 | Il Negozio (interno) | Entri nel magazzino, troppi PC, file col tuo nome |
| 5 | Lo Schermo | Sul monitor del negozio: un'immagine che non dovresti vedere |
| 6 | La Cucina (passato) | Il PC salta al passato — cucina della casa d'infanzia |
| 7 | Il Soggiorno (passato) | Foto di famiglia con una faccia tagliata |
| 8 | La Camera (infanzia) | Qualcuno che legge sul letto — ma piange |
| 9 | Il Corridoio (notte) | Una valigia, una porta, qualcuno che se ne va |
| 10 | La Strada | Fari che svaniscono, figura sulla soglia |
| 11 | La Scuola | Un amico che hai smesso di nominare |
| 12 | L'Ospedale | Stanza chiusa, letto, mano che non molla |
| 13 | Il Funerale | Sei lì, ma in disparte — non ti avvicini |
| 14 | La Stanza del PC (ritorno) | Stessa stanza del liv. 1, figura dietro di te |
| 15 | Lo Specchio | Riflesso non solo tuo |
| 16 | La Camera (presente) | Dormi, qualcuno veglia sul bordo del letto |
| 17 | La Soffitta | PC vecchio, loop di ricordi |
| 18 | L'Ultima Foto | Foto sbiadita, prima che tutto cambiasse |
| 19 | Il Biglietto | Torni all'ingresso. *"Ricordi?"* — Sì. *"Grazie."* |

### La Figura — La Morte

Il protagonista **non sa** che è la Morte. La percepisce come un mostro. Il giocatore capisce prima di lui.

| Fase | Livelli | Aspetto (per AI) | Percezione del protagonista |
|------|---------|------------------|-------------------------------|
| **Mostro** | 0–5 | Sagoma alta, cappuccio, arti troppo lunghi, volto nero o teschio sfocato, contorno frastagliato | *"C'è qualcosa lì. Non è umano."* |
| **Presenza** | 6–13 | Più definita: cappotto scuro, mani scheletriche, immobile, sempre ai margini | *"Mi segue. Ogni volta più vicina."* |
| **Riconoscimento** | 14–19 | Death classica ma sobria — figura alta, oscura, non aggressiva, occhi vuoti o stelle | *"Non voleva farmi male. Aspettava solo."* |

- **Regole visive:** reference sheet unica; mai in primo piano fino al liv. 14+; sempre ai margini o nei riflessi.
- **Non è un jumpscare:** osserva, aspetta, si avvicina. Horror psicologico, non splatter.

---

## 2. Regole composizione FLAT

### Perché

I livelli 0–2 hanno prospettiva profonda (corridoio, paesaggio). Rigenerare 15 differenze + versione distorted su scene con profondità è quasi impossibile con AI. **Da livello 2 in poi: composizione piatta.**

### Tipi di inquadratura ammessi

| Tipo | Descrizione | Esempio d'uso |
|------|-------------|---------------|
| **Parete frontale** | Una sola parete, ortogonale, oggetti appoggiati/appesi | Soggiorno, camera, bagno |
| **Tavolo top-down** | Vista dall'alto 90°, oggetti sul piano | Cucina, scrivania, funerale (tavolo con candele) |
| **Cornice finestra** | Finestra come rettangolo centrale, interno ai lati | Livello strada, livello finestra |
| **Vetrina frontale** | Negozio visto di fronte, senza interno profondo | Esterno negozio PC |
| **Scaffale frontale** | Parete piena di scaffali/oggetti | Magazzino, soffitta |
| **Schermo / foto** | Immagine dentro immagine (meta) | Livello 5, 18 |
| **Pannello collage** | Più foto appese su un muro | Livello 18 |

### Vietato

- Corridoi con punto di fuga (one-point perspective)
- Paesaggi con montagne/sfondo sfumato
- Interni con stanze visibili in profondità attraverso porte aperte
- Angolazioni 3/4 con più pareti visibili

### Checklist per ogni background

- [ ] Un solo piano focale principale
- [ ] Oggetti disposti su un piano (parete o tavolo)
- [ ] Nessuna linea di fuga verso il centro
- [ ] Dettagli leggibili anche in piccolo (per le differenze)
- [ ] Stessa risoluzione e aspect ratio per Original e Distorted
- [ ] **Original** = versione "normale" (luce neutra, pulita)
- [ ] **Distorted** = stessa composizione, ma atmosfera horror (più scura, desatura, crepe, muffa) — le differenze si applicano come overlay sprite, non nel JPG base

### Aspect ratio consigliato

Mantenere lo stesso dei livelli 0–1 (verificare in Unity). Probabilmente **16:9** o **4:3**. Da confermare prima di generare in batch.

---

## 3. Sistema delle 15 differenze

### Come funziona nel gioco

- Ogni livello ha **15 slot** (`Constants.MAX_DIFFERENCES = 15`) — tutti popolati con sprite
- Le differenze **attive** (`mustBeFound: 1`) dipendono dalla difficoltà del livello
- Ogni differenza attiva ha: `startedSprite` (normale) + `distortedSprite` (horror)
- Il background JPG è uguale tra sinistra e destra; le differenze sono **overlay sprite** posizionati sopra

### Difficoltà e differenze attive

| Difficoltà | Attive | Livelli |
|------------|--------|---------|
| **Facile** | **6** | 0–7 |
| **Medio** | **10** | 8–13 |
| **Difficile** | **14** | 14–19 |

Nei livelli **Medio** e **Difficile**, le 6 differenze "core" (marcate ✓ nelle tabelle) restano attive; si aggiungono rispettivamente **+4** e **+8** slot secondo la colonna "Extra" di ogni livello.

### Escalation horror (ordine delle differenze trovate)

| Progresso | Tipo di cambiamento | Esempio |
|-----------|---------------------|---------|
| Prime trovate | Dettaglio innocuo | Fiore cambia colore, quadro storto |
| Metà | Oggetto fuori posto | Chiavi, candela, testo sul biglietto |
| Avanzate | Presenza / mostro | Ombra, silhouette della Morte, riflesso |
| Ultime | Rivelazione | La Figura, *"Ricordi?"*, dettagli del ricordo della morte |

### Categorie di differenze (per i 15 slot)

Distribuire i 15 slot così (anche se solo 6–8 attivi):

| Slot | Categoria | Note |
|------|-----------|------|
| 1–3 | Oggetto piccolo | Orologio, tazza, libro, pianta |
| 4–6 | Oggetto medio | Lampada, cornice, borsa, TV |
| 7–9 | Testo/scritta | Biglietto, cartello, schermo PC |
| 10–12 | Presenza/ombra | Figura, riflesso, ombra sbagliata |
| 13–15 | Rivelazione forte | Volto, mano, *"Ricordi?"*, occhi |

---

## 4. Riepilogo livelli

| Lv | Titolo | Diff. | Inquadratura FLAT | Atmosfera Original | Atmosfera Distorted |
|----|--------|-------|-------------------|--------------------|---------------------|
| 0 | Il Pacco | 6 | Ingresso casa unica (da correggere) | Soglia, pacco, luce calda | Ombre lunghe, senso di attesa |
| 1 | La Stanza del PC | 6 | Parete scrivania | Setup gamer, monitor acceso | Atmosfera che cambia, glitch |
| 2 | Il Corridoio | 6 | Parete frontale con porta | Casa accogliente, luce calda | Parete scrostata, ragnatele |
| 3 | Negozio (esterno) | 6 | Vetrina frontale | Notte, luci calde, chiuso | Insegna rotta, ombre |
| 4 | Negozio (interno) | 6 | Scaffale PC frontale | Negozio vintage ordinato | Polvere, monitor glitch |
| 5 | Lo Schermo | 6 | Schermo CRT pieno frame | Collage digitale surreale | Volti nei floppy, occhi |
| 6 | Cucina (passato) | 6 | Tavolo top-down | Tavola apparecchiata | Piatti rovesciati, buio |
| 7 | Soggiorno (passato) | 6 | Parete con TV e foto | Anni '90, caldo | TV static, foto strappata |
| 8 | Camera (infanzia) | 10 | Parete con letto e poster | Colori pastello | Poster strappati, mostro |
| 9 | Corridoio (notte) | 10 | Parete con porta e appendiabiti | Notte, luce fioca | Porta socchiusa, valigia |
| 10 | La Strada | 10 | Cornice finestra | Notte, lampioni | Figura-Morte sul marciapiede |
| 11 | La Scuola | 10 | Parete cortile (muro + panchina) | Giorno, autunno | Graffiti, ombre |
| 12 | L'Ospedale | 10 | Parete corridoio (targa + porta) | Pulito, fluorescente | Porta socchiusa, fiori morti |
| 13 | Il Funerale | 10 | Interno chiesa frontale (banco) | Candele, fiori | Bara socchiusa, candele spente |
| 14 | Stanza PC (ritorno) | 14 | Parete scrivania (come L1) | Come liv. 1 | Morte dietro sedia |
| 15 | Lo Specchio | 14 | Bagno frontale (lavandino + specchio) | Pulito | Riflesso: la Morte |
| 16 | Camera (presente) | 14 | Parete con letto | Notte, lampada | Morte sul bordo letto |
| 17 | La Soffitta | 14 | Scaffale con scatoloni e PC | Polvere, luce finestra | Loop ricordo, PC acceso |
| 18 | L'Ultima Foto | 14 | Collage di foto su muro | Foto sbiadite, felici | Volti cancellati |
| 19 | Il Biglietto | 14 | **Stesso ingresso del L0** | Alba, pace | Callback casa unica |

---

## 5. Dettaglio livello per livello

### Convenzione difficoltà nelle tabelle

| Simbolo | Significato |
|---------|-------------|
| ✓ | Attiva in **Facile** (6 totali) |
| M | Attiva anche in **Medio** (+4 → 10 totali) |
| D | Attiva anche in **Difficile** (+4 → 14 totali) |

Sotto ogni tabella: **Extra Medio** e **Extra Difficile** indicano gli slot da attivare oltre ai 6 core.  
Per i livelli 0–1 (già implementati): **6 attive**, stesse regole Facile.

### Livelli 0–1 (completati)

| Lv | Diff. | Note narrative |
|----|-------|----------------|
| 0 | 6 | Ricordo inizia: pacco, PC, casa unica (asset da correggere) |
| 1 | 6 | PC in stanza, fattura, prima percezione del "mostro" |

---

### Livello 2 — Il Corridoio

**Difficoltà:** Facile 6 · Medio 10 · Difficile 14  
**Extra Medio:** 4, 8, 11, 13 · **Extra Difficile:** 7, 9, 14, 15

**Trama**
- *Intro:* Il PC mostra il corridoio di casa. Le ombre sono troppo lunghe. In fondo, qualcosa si muove — un mostro? No. Non puoi guardare troppo a lungo.
- *Outro:* Le ombre erano solo luce. O forse no. Lo schermo cambia: un negozio. Quello della fattura che hai trovato nel PC.

**Immagine — Composizione FLAT**

> ⚠️ L'asset attuale (`Level_002`) ha profondità. **Rigenerare** come parete frontale.

**Prompt Original:** `Frontal flat view of a home entryway wall. Wooden console table with a lamp, a snake plant, keys on a tray, framed family photos in a cluster, coat hooks with a tote bag and scarf, oval mirror, closed white door with brass handle, "HOME" doormat on hardwood floor. Warm beige tones, cozy modern rustic. No depth perspective, single flat wall, orthographic, illustration style, 16:9`

**Prompt Distorted:** `Same composition. Decaying wall with peeling wallpaper exposing brick, cobwebs, cracked photo frames, dead plant, broken lamp, door slightly ajar with dark gap, mold stains, cold blue-green tint. Same flat frontal view, same object positions. Horror atmosphere, no gore.`

**15 Differenze**

| # | Zona | Normale (started) | Distorta (horror) | Attiva |
|---|------|-------------------|-------------------|--------|
| 1 | Console sx | Pianta verde | Pianta appassita/nera | ✓ |
| 2 | Console centro | Lampada accesa | Lampada spenta/crack | ✓ |
| 3 | Console dx | Chiavi sul vassoio | Chiavi + biglietto *"Ricordi?"* | ✓ |
| 4 | Muro alto | 8 cornici foto | 9 cornici (+ foto bambino) | M |
| 5 | Muro centro | Specchio pulito | Specchio: mostro sfocato (Morte) | ✓ |
| 6 | Porta | Porta chiusa | Porta socchiusa, buio | ✓ |
| 7 | Porta | Tappetino "HOME" | Tappetino macchiato | D |
| 8 | Ganci | Borsa beige | Borsa aperta, foglio dentro | M |
| 9 | Ganci | Sciarpa | Sciarpa che pende come capelli | D |
| 10 | Soffitto | Lampadario normale | Lampadario che oscilla | |
| 11 | Pavimento | Parquet pulito | Macchia scura | M |
| 12 | Cornice | Foto famiglia sorridente | Foto: volto cancellato | ✓ |
| 13 | Angolo | Nessuno | Ragnatela grande | M |
| 14 | Specchio | Riflesso vuoto | Riflesso: occhi | D |
| 15 | Porta bassa | Zoccolo pulito | Graffio lungo | D |

---

### Livello 3 — Il Negozio (esterno)

**Trama**
- *Intro:* Il negozio della fattura. Chiuso. Luci spente. Ma qualcosa si muove dietro la tapparella.
- *Outro:* Nessuno risponde. Senti un rumore dal retro. C'è un ingresso laterale.

**Immagine**

> ✅ Asset `Level_003` (vetrina) è quasi frontale. **Adattabile** con ritaglio/crop per eliminare la profondità della strada.

**Prompt Original:** `Frontal flat view of a closed Italian electronics shop at night. Sign "ELETTRODOMESTICI & TECNOLOGIA", metal shutters half down, "Chiuso" sign, cobblestone street only in bottom 10%. Warm street lamp. Flat facade, no street depth. 16:9 illustration`

**Prompt Distorted:** `Same flat facade. Broken sign letter, shutter dented, "Chiuso" sign turned to "Aiuto", shadow figure behind shutter slats, flickering lamp, cracked window. Cold blue tint.`

**15 Differenze** *(basate SOLO su oggetti presenti in Original_3 / Disturbed_3)*

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Insegna | Lettere `ELETTRODOMESTICI` integre | Lettere R/O incrinate | ✓ |
| 2 | Vetrina sx | Tapparella come Original | Tapparella storta/piegata | ✓ |
| 3 | Dentro vetrina sx | Silhouette elettrodomestico | Sagoma umana alta (mostro) | ✓ |
| 4 | Vetrina dx | Tapparella come Original | Tapparella più bassa/ammaccata | |
| 5 | Cartello sx | `CHIUSO` | `AIUTO` | ✓ |
| 6 | Cartello dx | `Aperto domani ore 9:00` | `Non aprire mai` | |
| 7 | Lanterna | Glow giallo caldo | Glow teal freddo | |
| 8 | Sottotitolo | `RIPARAZIONI - ASSISTENZA` integro | Lettere danneggiate | |
| 9 | Vetro sx | Vetro integro | Crepe a ragno | ✓ |
| 10 | Cielo | Luna crescente | Luna più luminosa/fredda | |
| 11 | Pavimento | Ciottoli caldi | Ciottoli teal umidi | |
| 12 | Sinistra | Pluviale nero | Pluviale con tinta teal | |
| 13 | Vetro dx | Vetro integro | Crepe a ragno | ✓ |
| 14 | Insegna | `& TECNOLOGIA` | `& MEMORIA` | |
| 15 | Dentro vetrina | Cavi sottili | Cavi più aggrovigliati | |

---

### Livello 4 — Il Negozio (interno)

**Trama**
- *Intro:* Il magazzino è pieno di PC. Troppi. Tutti uguali al tuo. Uno è acceso.
- *Outro:* Sul monitor c'è un file col tuo nome. Lo apri. Lo schermo cambia immagine.

**Immagine**

> ⚠️ Asset `Level_004` ha profondità (scaffali + retro). **Rigenerare** come scaffale frontale.

**Prompt Original:** `Frontal flat view of a vintage PC repair shop wall. Wooden shelves packed with old computers, CRT monitors, floppy disks, HP/Sony boxes. Sign "VENDITA E RIPARAZIONI & TECNOLOGIA P.C." One monitor on displaying Windows desktop. Warm tungsten light. Flat orthographic, no depth, 16:9`

**Prompt Distorted:** `Same flat wall. Monitors showing static/glitch, boxes fallen, dust, cobwebs, one screen shows a face, green terminal text "RICORDI?", broken fluorescent light.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Scaffale alto | 5 scatole Sony | 6 scatole | |
| 2 | Monitor 1 | Desktop Windows | Schermo nero | ✓ |
| 3 | Monitor 2 | Foglio Excel | File: "tuo_nome.pdf" | ✓ |
| 4 | Monitor 3 | Test pattern | Volto nel static | ✓ |
| 5 | Scrivania | Tastiera | Tastiera + biglietto | |
| 6 | Muro | Cartello "Operatori 9:00" | "Non tornare" | ✓ |
| 7 | Scaffale | Floppy impilati | Floppy con faccia | |
| 8 | Pavimento | Cavi ordinati | Cavo che striscia | |
| 9 | Lampada | Accesa | Sfarfalla | |
| 10 | PC tower | Spento | LED rosso acceso | ✓ |
| 11 | Scaffale basso | Manuale | Diario aperto | |
| 12 | Angolo | Nessuno | La Figura tra scatole | ✓ |
| 13 | Monitor 1 | Nessun riflesso | Riflesso occhi | |
| 14 | Insegna | "& TECNOLOGIA P.C." | "& IL TUO PASSATO" | |
| 15 | Sotto scrivania | Vuoto | Scarpa | |

---

### Livello 5 — Lo Schermo

**Trama**
- *Intro:* Il monitor si accende da solo. Non mostra il desktop. Mostra... te. In un posto che non riconosci.
- *Outro:* L'immagine cambia. Una cucina. Una cucina di tanti anni fa.

**Immagine**

> ✅ Asset `Level_005` (schermo CRT) è già piatto. **Ottimo modello** per questo tipo di composizione.

**Prompt Original:** `Full frame of a vintage CRT monitor screen. Beige frame visible at edges. Screen shows surreal collage of floppy disks, gears, geometric shapes, cables, soft white glow center. Retro tech aesthetic. Flat, 16:9`

**Prompt Distorted:** `Same CRT frame. Screen glitched: human eyes in circuit board, face on floppy disk, melting colors, clock stopped at 3:33, dark vignette, static noise.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Centro | Luce bianca | Luce rossa | ✓ |
| 2 | Floppy sx | Disco blu | Disco con occhio | ✓ |
| 3 | Floppy dx | Disco standard | Disco con volto | |
| 4 | Orologio | 1:52 | 3:33 | ✓ |
| 5 | Cavo | Grigio | Cavo che si muove | |
| 6 | Prisma | Arcobaleno | Nero | |
| 7 | PCB | Circuito normale | Occhi nel chip | ✓ |
| 8 | Uccello meccanico | Presente | Occhio umano | |
| 9 | Blob | Blu/rosa | Rosso scuro | |
| 10 | Centro | Collage caotico | Foto cucina (preview) | ✓ |
| 11 | Bordo schermo | Pulito | Crack nel vetro | |
| 12 | Angolo | Nessuno | Testo "RICORDI?" | ✓ |
| 13 | Floppy basso | Etichetta bianca | Nome tuo | |
| 14 | Ingranaggio | Intero | Ruggine | |
| 15 | Cornice monitor | Beige | Beige + polvere nera | |

---

### Livello 6 — La Cucina (passato)

**Trama**
- *Intro:* La cucina di quella casa. Tavola per due. Un posto vuoto. Una lettera sul tavolo.
- *Outro:* La lettera ha un nome. Un nome che non pronunci da anni.

**Immagine — Top-down**

**Prompt Original:** `Top-down flat view of a rustic kitchen wooden table. Two place settings (plates, glasses, cutlery), bread, wine bottle, folded letter, checkered tablecloth. Warm afternoon light. No perspective, 90 degree overhead, 16:9`

**Prompt Distorted:** `Same overhead view. One plate flipped, wine spilled like blood, letter opened with scribbles, rotting fruit, cold desaturated colors, cockroach.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Posto sx | Piatto pieno | Piatto vuoto | ✓ |
| 2 | Posto dx | Bicchiere pieno | Bicchiere rovesciato | ✓ |
| 3 | Centro | Lettera piegata | Lettera aperta | ✓ |
| 4 | Centro | 2 posate | 3 posate | |
| 5 | Angolo | Pane intero | Pane muffa | |
| 6 | Angolo | Vaso fiori | Fiori appassiti | |
| 7 | Bottiglia | Vino | Vino versato | ✓ |
| 8 | Tovaglia | Motivo intatto | Macchia scura | |
| 9 | Sedia | Due visibili | Una spostata | |
| 10 | Bordo | Nessuno | Ombra mano | |
| 11 | Lettera | Sigillo | Sigillo rotto | |
| 12 | Posto dx | Sedia vuota | Sedia con cappotto | ✓ |
| 13 | Angolo | Nessuno | La Figura (solo mano) | ✓ |
| 14 | Piatto | Normale | Crepa nel piatto | |
| 15 | Bicchiere | Acqua | Acqua nera | |

---

### Livello 7 — Il Soggiorno (passato)

**Trama**
- *Intro:* Il soggiorno. TV con le antenne. Foto di famiglia. Qualcuno in più nella foto.
- *Outro:* Ogni differenza era un pezzo che avevi rimosso. La camera da letto ti aspetta.

**Immagine — Parete frontale**

**Prompt Original:** `Frontal flat wall of a 1990s living room. Old CRT TV on wooden stand, family photo frames on wall, worn sofa arm visible at bottom, lace curtains on sides, warm lamp. Flat orthographic, 16:9 illustration`

**Prompt Distorted:** `Same wall. TV static snow, photo frames cracked, one face scratched out, lamp flickering, water stain on wall, cold atmosphere.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | TV | Cartone animato | Static/neve | ✓ |
| 2 | Foto grande | 4 persone | 5 persone (+ Figura) | ✓ |
| 3 | Foto piccola | Bambino | Bambino senza occhi | |
| 4 | Lampada | Accesa | Spenta | ✓ |
| 5 | Divano | Cuscino | Cuscino indentato (qualcuno seduto) | ✓ |
| 6 | Tenda | Chiusa | Socchiusa + ombra | |
| 7 | Mensola | Vaso | Vaso rotto | |
| 8 | TV stand | Telecomando | Telecomando + biglietto | |
| 9 | Muro | Carta da parati fiori | Carta strappata | |
| 10 | Angolo | Nessuno | Ragno | |
| 11 | Foto media | Sorridente | Volto cancellato | ✓ |
| 12 | Pavimento | Tappeto | Tappeto storto | |
| 13 | TV stand | Nessuno | Orecchino | |
| 14 | Cornice | Vetro intatto | Vetro incrinato | |
| 15 | Soffitto | Pulito | Macchia umidità | ✓ |

---

### Livello 8 — La Camera (infanzia)

**Trama**
- *Intro:* La tua camera da bambino. Qualcuno sul letto che legge. Ma il libro è chiuso. E piange.
- *Outro:* *"Perché non mi hai fermato?"* — leggi sulle labbra.

**Immagine — Parete frontale**

**Prompt Original:** `Frontal flat view of a child's bedroom wall. Bunk bed, colorful posters (space, animals), stuffed animals on bed, small desk with lamp, nightlight. Warm, innocent. Flat, 16:9`

**Prompt Distorted:** `Same room. Posters torn, stuffed animal headless, bed empty but indent, desk lamp red, shadow figure on bed edge, cold blue.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Letto | Peluche orsacchiotto | Peluche senza testa | ✓ |
| 2 | Letto | Libro aperto | Libro chiuso | ✓ |
| 3 | Letto | Nessuno | Sagoma seduta | ✓ |
| 4 | Poster 1 | Astronave | Astronave in fiamme | ✓ |
| 5 | Poster 2 | Gatto | Gatto con occhi rossi | |
| 6 | Scrivania | Lampada gialla | Lampada rossa | ✓ |
| 7 | Scrivania | Quaderno | Quaderno: disegno famiglia | |
| 8 | Mensola | Trofeo | Trofeo rovesciato | |
| 9 | Letto basso | Coperta azzurra | Coperta con macchia | |
| 10 | Angolo | Nessuno | Ombra lunga | M |
| 11 | Parete | Adesivo stella | Adesivo nero | |
| 12 | Sotto letto | Vuoto | Scarpa adulta | ✓ |
| 13 | Finestra (laterale) | Stelle | Sagoma esterna | M |
| 14 | Cuscino | Asciutto | Bagnato (lacrime) | M |
| 15 | Porta | Chiusa | Socchiusa + luce | M |

---

### Livello 9 — Il Corridoio (notte)

**Trama**
- *Intro:* Di notte. Una valigia. Un cappotto. Qualcuno che se ne va. Tu guardi dalla porta della camera.
- *Outro:* *"Eri tu. Eri tu che non hai detto nulla."*

**Immagine — Parete frontale (notte)**

**Prompt Original:** `Frontal flat wall of a dark apartment hallway at night. Coat hooks with jacket and scarf, small table with keys and phone, closed apartment door with number, doormat, dim nightlight. Blue moonlight. Flat, 16:9`

**Prompt Distorted:** `Same wall. Door ajar with light behind, jacket missing (only hanger), suitcase by door, phone screen lit showing missed calls, cold atmosphere.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Porta | Chiusa | Socchiusa | ✓ |
| 2 | Pavimento | Vuoto | Valigia | ✓ |
| 3 | Ganci | Cappotto | Solo gruccia | ✓ |
| 4 | Tavolino | Chiavi | Chiavi + lettera | M |
| 5 | Tavolino | Telefono spento | Telefono: 12 chiamate perse | ✓ |
| 6 | Muro | Quadro | Quadro storto | M |
| 7 | Numero porta | "12" | "12" capovolto | |
| 8 | Zerbino | Pulito | Zerbino fuori posto | |
| 9 | Soffitto | Lampada spenta | Lampada fioca | M |
| 10 | Angolo | Nessuno | Ombra valigia | M |
| 11 | Porta | Spioncino chiuso | Spioncino aperto | |
| 12 | Pavimento | Parquet | Scia fangosa | |
| 13 | Angolo basso | Nessuno | Scarpa donna | ✓ |
| 14 | Muro | Intonaco ok | Graffio | |
| 15 | Dietro porta | Buio | Sagoma | ✓ |

---

### Livello 10 — La Strada

**Trama**
- *Intro:* La strada. I fari si allontanano. Qualcuno sulla soglia non corre dietro.
- *Outro:* *"Non è colpa tua"* — o forse sì.

**Immagine — Cornice finestra**

**Prompt Original:** `View through a window frame (flat). Night street, one car with headlights driving away, figure standing on doorstep in distance, warm interior curtains on sides. Flat composition, 16:9`

**Prompt Distorted:** `Same window frame. Car gone, figure closer on sidewalk, looking at camera, rain, cold blue, condensation on glass.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Strada | Auto con fari | Auto sparita | ✓ |
| 2 | Marciapiede | Figura piccola | Figura più grande | ✓ |
| 3 | Soglia | Vuota | Figura in cappotto | ✓ |
| 4 | Cielo | Stelle | Nuvole nere | |
| 5 | Lampione | Acceso | Spento | M |
| 6 | Vetro | Pulito | Condensa "AIUTO" | ✓ |
| 7 | Tenda sx | Aperta | Chiusa | |
| 8 | Davanzale | Vuoto | Candela spenta | |
| 9 | Strada | Asciutta | Bagnata | ✓ |
| 10 | Figura | Di spalle | Di fronte | M |
| 11 | Auto | 2 fari | 1 faro | |
| 12 | Cespuglio | Normale | Sagoma dietro | |
| 13 | Cielo | Luna | Luna rossa | M |
| 14 | Finestra | Cornice bianca | Cornice scrostata | M |
| 15 | Angolo | Nessuno | La Figura vicina | ✓ |

---

### Livello 11 — La Scuola

**Trama**
- *Intro:* Il cortile. Tu in disparte. Qualcuno accanto a te che ride. Poi non c'è più stato.
- *Outro:* *"Ti ricordi di me?"*

**Immagine — Parete cortile**

**Prompt Original:** `Frontal flat view of a school courtyard wall. Brick wall, wooden bench, backpack on bench, autumn leaves on ground, basketball hoop above, cloudy sky only in top 15%. Flat, 16:9`

**Prompt Distorted:** `Same wall. Graffiti "RICORDI", bench broken, backpack open with diary, leaves black, hoop net torn, grey sky.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Panca | Zaino | Zaino aperto | ✓ |
| 2 | Panca | Vuota | Due indentazioni | ✓ |
| 3 | Muro | Mattoni puliti | Graffiti | ✓ |
| 4 | Muro | Nessun poster | Poster scomparse | |
| 5 | Canestro | Rete intatta | Rete strappata | M |
| 6 | Foglie | Arancioni | Nere | M |
| 7 | Zaino | Tasche chiuse | Diario fuori | ✓ |
| 8 | Diario | — | Nome amico cancellato | M |
| 9 | Pavimento | Pulito | Disegno gesso (due figure) | |
| 10 | Angolo | Nessuno | Ombra bambino | ✓ |
| 11 | Muro alto | Intonaco ok | Crepa | |
| 12 | Panca | Nessuno | Fionda | |
| 13 | Cielo | Nuvole | Uccelli neri | |
| 14 | Angolo | Nessuno | La Figura (lontana) | M |
| 15 | Zaino | Portachiavi | Portachiavi + foto | ✓ |

---

### Livello 12 — L'Ospedale

**Trama**
- *Intro:* Corridoio ospedale. Porta chiusa. Ma nell'immagine è socchiusa. Qualcuno nel letto.
- *Outro:* *"Dove eri?"*

**Immagine — Parete corridoio (piatta)**

**Prompt Original:** `Frontal flat hospital corridor wall. Door with room number "312", hand sanitizer dispenser, bench, fluorescent light, clean white/beige. Medical poster on wall. Flat orthographic, 16:9`

**Prompt Distorted:** `Same wall. Door ajar with dark inside, flickering light, bench with abandoned flowers, poster torn, rust stain, blood drop on floor (subtle).`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Porta | Chiusa | Socchiusa | ✓ |
| 2 | Porta | Numero "312" | "312" scritto a mano | M |
| 3 | Interno porta | Buio | Luce verde | ✓ |
| 4 | Panca | Vuota | Fiori appassiti | ✓ |
| 5 | Panca | — | Cappotto | M |
| 6 | Distributore | Pieno | Vuoto | |
| 7 | Poster | Salute | Volto barrato | ✓ |
| 8 | Pavimento | Pulito | Goccia scura | M |
| 9 | Luce | Fluorescente | Sfarfalla | |
| 10 | Angolo | Nessuno | Ombra sedia a rotelle | |
| 11 | Maniglia | Cromata | Macchiata | M |
| 12 | Porta bassa | Fessura | Liquido | |
| 13 | Parete | Pulita | Graffio "perdonami" | ✓ |
| 14 | Soffitto | Piastrella ok | Piastrella storta | |
| 15 | Interno | Nessuno | Mano sulla porta | ✓ |

---

### Livello 13 — Il Funerale

**Trama**
- *Intro:* Il funerale. Un posto vuoto che doveva essere tuo. Ma nell'immagine ci sei. In fondo. In disparte.
- *Outro:* La bara si apre. È vuota. La Figura è lì.

**Immagine — Chiesa frontale (banco)**

**Prompt Original:** `Frontal flat view of a church pew and altar area. Wooden pew in foreground, candles on altar, flowers, black ribbon, soft warm light through stained glass (only as flat colored panels, no depth). 16:9`

**Prompt Distorted:** `Same composition. Candles extinguished, flowers dead, pew empty with name tag, cold grey light, black petals on floor.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Altare | 5 candele accese | 3 spente | ✓ |
| 2 | Altare | Fiori bianchi | Fiori neri | ✓ |
| 3 | Banco | Vuoto | Biglietto con il tuo nome | ✓ |
| 4 | Banco | Libro preghiere | Libro aperto su tuo nome | M |
| 5 | Pavimento | Pulito | Petalo nero | |
| 6 | Vetro colorato | Blu/verde | Rosso | |
| 7 | Altare | Foto defunto | Foto sfocata | ✓ |
| 8 | Angolo | Nessuno | Figura in nero (dietro) | ✓ |
| 9 | Banco davanti | Nessuno | Cappello | M |
| 10 | Candela | Fiamma | Fumo | M |
| 11 | Nastro | Nero | Bianco | |
| 12 | Parete | Croce | Croce capovolta | |
| 13 | Altare | Bara chiusa | Bara socchiusa | ✓ |
| 14 | Pavimento | Nessuno | Ombra banco | M |
| 15 | Banco | Posto libero | Posto con giacca (la tua) | |

---

### Livello 14 — La Stanza del PC (ritorno)

**Trama**
- *Intro:* Sei tornato alla tua stanza. Il PC mostra te. Seduto. Con qualcuno dietro.
- *Outro:* La Figura è più vicina. Lo specchio ti aspetta.

**Immagine — Parete scrivania (riuso concettuale L1)**

**Prompt Original:** `Frontal flat gamer desk wall. Brick wall, monitor, speakers, skull poster, red LED strips, chair back visible. Same layout as level 1 but slightly different objects arrangement. 16:9`

**Prompt Distorted:** `Same desk. Monitor shows your face, LED strips flickering, skull poster eyes glowing, figure silhouette behind chair, oppressive red.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Monitor | Paesaggio | Il tuo volto | ✓ |
| 2 | Monitor | — | Figura dietro te nel video | ✓ |
| 3 | Sedia | Vuota | Indentata | ✓ |
| 4 | Poster teschio | Normale | Occhi rossi | M |
| 5 | LED | Rossi | Rossi che pulsano | ✓ |
| 6 | Casse | Normali | Vibrazione (blur) | M |
| 7 | Scrivania | Mouse | Mouse + biglietto "Ricordi?" | ✓ |
| 8 | Parete | Mattoni | Mattoni con crepa | M |
| 9 | Angolo | Nessuno | La Figura dietro sedia | ✓ |
| 10 | Laptop | Schermo paesaggio | Schermo nero | M |
| 11 | Pianta | Verde | Morta | D |
| 12 | Controller | Posizione A | Posizione B | D |
| 13 | Monitor | Nessun riflesso | Occhi nel riflesso | D |
| 14 | Sotto scrivania | Vuoto | Scarpa | D |
| 15 | Cornice | Poster | Poster: foto famiglia | |

---

### Livello 15 — Lo Specchio

**Trama**
- *Intro:* Il bagno. Il riflesso non è solo tuo. La Figura è dietro. Non minaccia. Aspetta.
- *Outro:* *"Basta scappare."*

**Immagine — Bagno frontale**

**Prompt Original:** `Frontal flat bathroom wall. White sink, mirror above, toothbrush cup, towel rack, small cabinet. Clean, neutral light. Mirror shows empty reflection area. 16:9`

**Prompt Distorted:** `Same bathroom. Mirror cracked, figure behind in reflection, red light, water dripping from faucet, towel on floor.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Specchio | Solo bagno | Figura dietro | ✓ |
| 2 | Specchio | Integro | Crepa a ragno | ✓ |
| 3 | Lavandino | Asciutto | Gocce rosse | ✓ |
| 4 | Rubinetto | Chiuso | Gocciola | ✓ |
| 5 | Tazza | 2 spazzolini | 3 spazzolini | M |
| 6 | Asciugamano | Appeso | A terra | D |
| 7 | Armadio | Chiuso | Socchiuso | M |
| 8 | Specchio | — | Scritta "guarda" | ✓ |
| 9 | Pavimento | Pulito | Asciugamano | D |
| 10 | Angolo | Nessuno | Ombra | M |
| 11 | Parete | Piastrella ok | Piastrella nera | |
| 12 | Specchio | Tu (normale) | Tu (occhi neri) | ✓ |
| 13 | Mensola | Sapone | Sapone + capello | D |
| 14 | Luce | Bianca | Rossa | M |
| 15 | Specchio angolo | Vuoto | Occhio | D |

---

### Livello 16 — La Camera (presente)

**Trama**
- *Intro:* La tua camera. Tu che dormi. Qualcuno sul bordo del letto che veglia.
- *Outro:* *"Va bene così."*

**Immagine — Parete letto**

**Prompt Original:** `Frontal flat bedroom wall. Double bed with messy sheets, nightstand with lamp and phone, alarm clock, window with curtains (flat). Night, warm lamp light. 16:9`

**Prompt Distorted:** `Same room. Figure sitting on bed edge (silhouette), lamp dim, phone screen lit, clock 3:33, cold blue moonlight mix.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Letto | Vuoto (dormi) | Sagoma seduta al bordo | ✓ |
| 2 | Letto | Coperta liscia | Indentazione seconda persona | ✓ |
| 3 | Comodino | Lampada accesa | Lampada fioca | M |
| 4 | Comodino | Telefono spento | Telefono: "ti ricordi?" | ✓ |
| 5 | Sveglia | 23:15 | 3:33 | ✓ |
| 6 | Finestra | Tenda chiusa | Socchiusa + ombra | M |
| 7 | Parete | Nessun quadro | Quadro famiglia | ✓ |
| 8 | Pavimento | Pulito | Scarpe | M |
| 9 | Angolo | Nessuno | La Figura | ✓ |
| 10 | Cuscino | Asciutto | Macchia | M |
| 11 | Armadio | Chiuso | Socchiuso | D |
| 12 | Soffitto | Pulito | Macchia | D |
| 13 | Letto | Una persona | Ombra di due | D |
| 14 | Comodino | Bicchiere acqua | Bicchiere vuoto | |
| 15 | Angolo | Nessuno | Biglietto "grazie" | D |

---

### Livello 17 — La Soffitta

**Trama**
- *Intro:* La soffitta. Scatoloni. Un PC vecchio. Lo schermo è acceso. Mostra questa stessa stanza.
- *Outro:* Loop. Ricordo dentro ricordo.

**Immagine — Scaffale frontale**

**Prompt Original:** `Frontal flat attic wall with wooden shelves. Cardboard boxes labeled "ESTATE", "TOYS", old CRT monitor, dusty toys, single window light from side. Warm dust particles. 16:9`

**Prompt Distorted:** `Same attic. Boxes open, photos scattered, old monitor showing the attic (recursive), cobwebs, cold light, figure between boxes.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Scatola 1 | Chiusa "ESTATE" | Aperta, foto | ✓ |
| 2 | Scatola 2 | "TOYS" | Vuota | |
| 3 | Monitor | Spento | Acceso (loop soffitta) | ✓ |
| 4 | Mensola | Orsacchiotto | Orsacchiotto senza occhi | ✓ |
| 5 | Pavimento | Pulito | Foto sparse | ✓ |
| 6 | Foto | Famiglia felice | Volto cancellato | ✓ |
| 7 | Angolo | Nessuno | La Figura | M |
| 8 | Finestra | Luce | Luce fredda | M |
| 9 | Scatola 3 | "DOCUMENTI" | Ribaltata | M |
| 10 | Ragnatela | Piccola | Grande | M |
| 11 | Monitor | — | Testo "ricordi?" | ✓ |
| 12 | Scaffale | Libro | Diario aperto | D |
| 13 | Tetto | Trave ok | Trave crepa | D |
| 14 | Giocattolo | Carro | Carro rotto | D |
| 15 | PC vecchio | Polvere | Schermo luminoso | D |

---

### Livello 18 — L'Ultima Foto

**Trama**
- *Intro:* L'ultima foto prima che tutto cambiasse. Sorridenti. In cucina. Dettagli che hai cancellato.
- *Outro:* Ogni differenza era una bugia che ti eri raccontato.

**Immagine — Collage foto su muro**

**Prompt Original:** `Frontal flat wall covered with pinned photographs. Polaroids and printed photos overlapping: family in kitchen, birthday, park. Warm nostalgic tones. Cork board texture. 16:9`

**Prompt Distorted:** `Same photo wall. Faces scratched out, photos burned at edges, one photo shows figure, red thread connecting photos, cold desaturated.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Foto centro | 2 persone felici | 3 persone (+ Figura) | ✓ |
| 2 | Foto centro | Volti visibili | Un volto cancellato | ✓ |
| 3 | Foto sx alto | Compleanno | Torta nera | M |
| 4 | Foto dx | Parco | Parco vuoto | ✓ |
| 5 | Polaroid | Bambino | Bambino che piange | ✓ |
| 6 | Bacheca | Spillo rosso | Filo rosso tra foto | M |
| 7 | Foto basso | Cucina | Cucina rovesciata | ✓ |
| 8 | Angolo | Nessuno | Foto bruciata | M |
| 9 | Polaroid | Data visibile | Data cancellata | D |
| 10 | Foto | Sorriso | Bocca cucita (subtle) | D |
| 11 | Spillo | 5 foto | 6 foto | |
| 12 | Foto | Mano saluto | Mano che gratta vetro | ✓ |
| 13 | Bordo | Bacheca sughero | Sughero strappato | D |
| 14 | Foto piccola | Coppia | Solo una persona | ✓ |
| 15 | Centro | Nessun testo | "perdonami" scritto | ✓ |

---

### Livello 19 — Il Biglietto

**Difficoltà:** Difficile 14  
**Extra Medio:** — (livello solo Difficile) · **Extra Difficile:** 4, 6, 7, 9, 11, 12, 13, 14

**Trama**
- *Intro:* Sei di nuovo alla soglia. Lo stesso ingresso. Lo stesso pacco. Lo stesso PC che avevi preso. Ora capisci: non stavi esplorando. Stavi rivivendo l'ultimo ricordo prima di morire.
- *Outro:* Sotto *"Ricordi?"* c'è *"Grazie."* La Morte non ti ha mai voluto spaventare. Aspettava che guardassi. Il loop si chiude. Il buio non fa più paura.

**Immagine — Stesso ingresso del Livello 0**

> Usare **la stessa composizione** del L0 (casa unica). L'asset Toscana attuale va sostituito su entrambi.

**Prompt Original:** `Frontal flat view of the protagonist's unique home entrance — SAME composition as level 0. Front door, doorstep, package, familiar details. Dawn light, peaceful, sense of closure. Flat orthographic, 16:9`

**Prompt Distorted:** `Same entrance as level 0. Package open, PC inside, warm golden dawn. Death figure in distance — not threatening, waving goodbye. Peace, acceptance, not horror.`

**15 Differenze**

| # | Zona | Normale | Distorta | Attiva |
|---|------|---------|----------|--------|
| 1 | Pacco | Chiuso | Aperto | ✓ |
| 2 | Biglietto | "Ricordi?" | "Ricordi?" + "Grazie" | ✓ |
| 3 | Porta | Chiusa | Aperta (luce dentro) | ✓ |
| 4 | Vaso fiori | Rossi | Appassiti | D |
| 5 | Rose | Rosa | Bianche | ✓ |
| 6 | PC (nel pacco) | — | Schermo spento | D |
| 7 | Zerbino | "HOME" | "HOME" consumato | D |
| 8 | Finestra | Buio | Luce calda | ✓ |
| 9 | Maniglia | Bronzo | Maniglia girata | D |
| 10 | Angolo | Nessuno | Morte che saluta | ✓ |
| 11 | Cielo (se visibile) | Alba | Alba dorata | D |
| 12 | Pacco | Nuovo | Consumato | D |
| 13 | Porta | Blu | Blu più chiaro | D |
| 14 | Soglia | Vuota | Biglietto caduto | D |
| 15 | Roseto | Foglie | Foglie dorate | |

---

## 6. Asset esistenti: tenere o rigenerare

| Cartella | Stato | Azione |
|----------|-------|--------|
| `Level_000` | Casa Toscana (prob. errata) | **Rigenerare** — casa unica del protagonista, stesso asset per L0 e L19 |
| `Level_002` | Corridoio con profondità | **Rigenerare** flat (parete frontale) |
| `Level_003` | Vetrina negozio | **Adattare** (crop, meno strada) |
| `Level_004` | Interno con profondità | **Rigenerare** flat (scaffale) |
| `Level_005` | Schermo CRT | **Tenere** come riferimento L5 |
| `Level_006` | Paesaggio con profondità | **Non usare** (non encaixa nella trama) |
| `Level_007` | Sconosciuto | Verificare; probabilmente scartare |

### Naming convention (confermata)

```
Sprites/Level_NNN/
  Original_N.jpg      (o .png)
  Distorted_N.jpg
  NNN_1.png           (sprite sheet differenza 1)
  NNN_2.png
  ...
```

---

## 7. Prompt style guide (AI)

### Stile visivo unificato

Aggiungere a ogni prompt:

```
Style: painted illustration, slightly stylized, NOT photorealistic.
Consistent warm-cool contrast between Original (warm) and Distorted (cold).
Horror psychological, no gore, no blood (except subtle suggestions).
Flat orthographic composition, single focal plane.
16:9 aspect ratio, 1920x1080.
Italian/European setting.
```

### Workflow generazione consigliato

1. **Generare Original** con prompt del livello
2. **Img2img o edit** per Distorted (stesso seed/composizione)
3. **Generare sprite differenze** separatamente su sfondo trasparente (normale + horror per ogni diff attiva)
4. **Posizionare** in Unity e annotare `normalizedPosition` nei `LevelData`

### Reference per la Morte (La Figura)

Creare **2 sheet reference**:

1. **Mostro (L0–5):** sagoma alta, cappuccio, arti innaturali, volto nero — il protagonista la vede così
2. **Morte (L6–19):** figura oscura elegante, mani scheletriche, immobile, non aggressiva

Usare la reference corretta in base alla fase del livello.

---

## 8. Decisioni confermate

| Decisione | Valore |
|-----------|--------|
| La Figura | **La Morte** — all'inizio percepita come mostro |
| Il PC | Oggetto preso dal protagonista **prima di morire**; il gioco è quel ricordo |
| Casa L0 | **Unica** — asset attuale probabilmente errato; L19 = stesso ingresso |
| Differenze attive | Facile **6** (L0–7) · Medio **10** (L8–13) · Difficile **14** (L14–19) |
| Testi | Allineati in `Assets/i18n/it.json` |

---

*Documento aggiornato — Horror Memory, livelli 0–19.*
