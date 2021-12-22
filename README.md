# TextAnalyze
In questa applicazione web è stata implementata la funzionalità dell'analisi di un testo attraverso l'utilizzo dei cognitive services di Azure. Il messaggio viene inserito in una coda che triggera una azure function, la quale richiama il cognitive service che restituisce un'analisi. Alcuni di questi dati vengono salvati su un database per poi essere visualizzati sulla pagina web.
