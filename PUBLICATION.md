# Configurazione privata

Le credenziali e i dati operativi devono restare fuori dal repository. Le impostazioni ASP.NET Core si configurano tramite variabili di ambiente (separatore doppio underscore) oppure dotnet user-secrets in sviluppo, indicando il progetto con --project. I valori vuoti nei file di esempio devono essere configurati prima di utilizzare i servizi corrispondenti. I file .env non vengono caricati automaticamente.

Non includere backup, esportazioni, log, chiavi private o dati personali nei commit.

La bonifica dei file correnti non elimina i valori presenti nella cronologia Git: prima di rendere pubblico il repository, revocare o ruotare le credenziali già versionate e bonificare la cronologia, gli altri branch/tag e gli eventuali allegati.

Il programma legge un file locale e genera emails_*.txt. Questi file contengono indirizzi estratti e sono esclusi da Git. Usare soltanto dati sintetici negli esempi. Non sono state individuate credenziali incorporate nei sorgenti correnti.
