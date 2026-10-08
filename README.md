# EmailExtractor

Tool da terminale che estrae indirizzi email da un file di testo e li salva in file separati, un indirizzo per riga. È utile per ricavare elenchi da testi o esportazioni locali senza dover aprire manualmente file molto grandi.

## Come funziona

Il programma legge il file una riga alla volta, cerca gli indirizzi con una espressione regolare e li raccoglie in blocchi da **2.000.000 di corrispondenze**. Ogni blocco viene scritto su disco; l'eventuale ultimo blocco contiene gli indirizzi rimanenti.

- Mantiene l'ordine di rilevamento.
- Mantiene anche gli indirizzi duplicati.
- Non verifica l'esistenza delle caselle né la possibilità di recapito.
- Non richiede database, servizi email o connessioni di rete.

La lettura è progressiva, ma ciascun blocco di indirizzi resta in memoria fino alla scrittura.

## Requisiti e avvio

Serve il .NET SDK compatibile con **.NET 8**. Dalla radice del repository:

~~~powershell
dotnet restore EmailExtractor.csproj
dotnet build EmailExtractor.csproj
dotnet run --project EmailExtractor.csproj
~~~

Quando richiesto, incollare il percorso del file e premere Invio:

~~~text
C:\dati-demo\testo.txt
~~~

Inserire il percorso senza virgolette, anche quando contiene spazi. Il percorso può essere assoluto oppure relativo alla directory corrente del terminale.

## Esempio

Contenuto di `testo.txt`:

~~~text
Contatto principale: alice@example.com
Altro contatto: bob@example.org
Ripetizione: alice@example.com
~~~

Contenuto di `emails_1.txt`:

~~~text
alice@example.com
bob@example.org
alice@example.com
~~~

Gli indirizzi sono esempi fittizi.

## File prodotti

Gli output si chiamano `emails_1.txt`, `emails_2.txt` e così via e vengono salvati nella **directory corrente del processo**, non necessariamente accanto al file di input.

Se non vengono trovate corrispondenze, non viene creato un nuovo file di output. I file con lo stesso nome vengono sovrascritti; file di esecuzioni precedenti con numeri superiori possono rimanere nella cartella. Usare quindi una cartella di lavoro dedicata per ogni estrazione quando si vogliono conservare i risultati.

Per eseguire da una cartella diversa, passare a `--project` il percorso assoluto del progetto.

## Limiti e dati locali

L'estrazione usa il pattern presente in [Program.cs](Program.cs): non è un parser completo di tutti i formati email possibili e non esegue deduplicazione o normalizzazione.

I risultati possono contenere dati personali e vanno conservati localmente. I file `emails_*.txt` sono esclusi dal controllo versione. Vedere [PUBLICATION.md](PUBLICATION.md) per le indicazioni sulla pubblicazione del sorgente.

## Flussi operativi e automazioni

Vedere [FLUSSI.md](FLUSSI.md) per i percorsi dall'azione iniziale al risultato, le operazioni interne, gli errori, gli effetti parziali e le automazioni attive o disattivate.
