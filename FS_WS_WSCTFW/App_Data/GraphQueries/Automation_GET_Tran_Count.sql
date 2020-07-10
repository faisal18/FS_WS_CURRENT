USE FS_WS_WSCTFW    ;
with 
pbm as (select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc ) rn 
   from monitoring_transactioncount    
   where  ApplicationName = 'PBMLink'   ),

erx as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn    
	from monitoring_transactioncount    
	where  ApplicationName = 'eRxPharmacy'   ) ,    

OIC as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn   
	from monitoring_transactioncount 
	where  ApplicationName = 'OIC - Eligiblity'  )    

select  
pbm.CheckingTime as 'PBM_time' ,
pbm.[Count] as 'PBM_Count' ,   
erx.[Count] as 'ERX_Count' ,   
OIC.[Count] as 'OIC_Count'  

from pbm    
full outer join erx on pbm.rn = erx.rn    
full outer join OIC on oic.rn = pbm.rn

where CONVERT(date,pbm.CheckingTime) > CONVERT(date,getdate()-2)
order by pbm.CheckingTime desc