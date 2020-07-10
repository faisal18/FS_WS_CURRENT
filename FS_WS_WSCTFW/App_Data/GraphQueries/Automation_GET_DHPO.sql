USE FS_WS_WSCTFW    ;
with 
CS_Downloaded as (select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc ) rn 
from monitoring_transactioncount    
where  ApplicationName = 'DHPO-CS_Downloaded'   ),

CS_NotDownloaded as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn    
from monitoring_transactioncount    
where  ApplicationName = 'DHPO-CS_NotDownloaded'   ) ,    

PA_Downloaded as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn   
from monitoring_transactioncount 
where  ApplicationName = 'DHPO-PA_Downloaded'  ) ,

PA_NotDownloaded as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn   
from monitoring_transactioncount 
where  ApplicationName = 'DHPO-PA_NotDownloaded'  )  ,

PR_Downloaded as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn   
from monitoring_transactioncount 
where  ApplicationName = 'DHPO-PR_Downloaded'  )  ,

PR_NotDownloaded as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn   
from monitoring_transactioncount 
where  ApplicationName = 'DHPO-PR_NotDownloaded'  )  ,

Total_CS as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn   
from monitoring_transactioncount 
where  ApplicationName = 'DHPO-Total_CS'  )  ,

Total_PA as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn   
from monitoring_transactioncount 
where  ApplicationName = 'DHPO-Total_PA'  )  ,

Total_PR as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn   
from monitoring_transactioncount 
where  ApplicationName = 'DHPO-Total_PR'  )  

select  TOP 1
Total_CS.CheckingTime as 'CheckingTime',
Total_CS.[Count] as 'DHPO_Total_CS' ,
CS_Downloaded.[Count] as 'DHPO_CS_Downloaded' ,
CS_NotDownloaded.[Count] as 'DHPO_CS_NotDownloaded',
Total_PA.[Count] as 'DHPO_Total_PA' ,
PA_Downloaded.[Count] as 'DHPO_PA_Downloaded' ,
PA_NotDownloaded.[Count] as 'DHPO_PA_NotDownloaded' ,
Total_PR.[Count] as 'DHPO_Total_PR'  ,
PR_Downloaded.[Count] as 'DHPO_PR_Downloaded' ,
PR_NotDownloaded.[Count] as 'DHPO_PR_NotDownloaded' 
from CS_Downloaded    
full outer join CS_NotDownloaded on CS_Downloaded.rn = CS_NotDownloaded.rn
full outer join PA_Downloaded on CS_Downloaded.rn = PA_Downloaded.rn
full outer join PA_NotDownloaded on CS_Downloaded.rn = PA_NotDownloaded.rn
full outer join PR_Downloaded on CS_Downloaded.rn = PR_Downloaded.rn
full outer join PR_NotDownloaded on CS_Downloaded.rn = PR_NotDownloaded.rn
full outer join Total_CS on CS_Downloaded.rn = Total_CS.rn
full outer join Total_PA on CS_Downloaded.rn = Total_PA.rn
full outer join Total_PR on CS_Downloaded.rn = Total_PR.rn