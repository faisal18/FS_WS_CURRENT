sqlcmd  -S 10.162.176.85 -d PBMM -U fsheikh -P Dell@300  -i "C:\Fazeel\\PayerIntegration\\OtherPayers\PayerIntegration-PriorAuthorization_Transaction_Observation.SQL" -o "C:\Fazeel\\PayerIntegration\\PR\\queryoutput.CSV" -W  -s,-h-1  