USE FS_WS_WSCTFW    
;with     

Claims as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'Claims' ),  

Clinician as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'Clinician' ),  

ClinicianValidate as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'Clinician Validate' ),  

DHPOValidate as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'DHPO Validate' ),  

Eauthorization as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'Eauthorization' ),  

ERXValidate as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'ERX Validate' ),  

ERXPharmacy as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'ERXPharmacy' ),  

LMUValidate as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'LMU Validate' ),  

MemberRegistration as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'Member Registration' ),  

PBMLink as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'PBMLink' ),  

PBMSwitchDimensions as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'PBMSwitch Dimensions' ),  

PBMSwitchPayer as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'PBMSwitch Payer' ),  

ShafafiyaValidate as (     select ApplicationName,CheckingTime,[Status],row_number() over (order by checkingtime desc) rn      
	from Monitoring_ApplicationStatuses     
	where  ApplicationName = 'Shafafiya Validate' )  

select
Claims.[CheckingTime] as 'Claims_Time' ,
Claims.[Status] as 'Claims_Status' ,
Clinician.[CheckingTime] as 'Clinician_Time' ,
Clinician.[Status] as 'Clinician_Status' ,
ClinicianValidate.[CheckingTime] as 'ClinicianValidate_Time' ,
ClinicianValidate.[Status] as 'ClinicianValidate_Status' ,
DHPOValidate.[CheckingTime] as 'DHPOValidate_Time' ,
DHPOValidate.[Status] as 'DHPOValidate_Status' ,
Eauthorization.[CheckingTime] as 'Eauthorization_Time' ,
Eauthorization.[Status] as 'Eauthorization_Status' ,
ERXValidate.[CheckingTime] as 'ERXValidate_Time' ,
ERXValidate.[Status] as 'ERXValidate_Status' ,
ERXPharmacy.[CheckingTime] as 'ERXPharmacy_Time' ,
ERXPharmacy.[Status] as 'ERXPharmacy_Status' ,
LMUValidate.[CheckingTime] as 'LMUValidate_Time' ,
LMUValidate.[Status] as 'LMUValidate_Status' ,
MemberRegistration.[CheckingTime] as 'MemberRegistration_Time' ,
MemberRegistration.[Status] as 'MemberRegistration_Status' ,
PBMLink.[CheckingTime] as 'PBMLink_Time' ,
PBMLink.[Status] as 'PBMLink_Status' ,
PBMSwitchDimensions.[CheckingTime] as 'PBMSwitchDimensions_Time' ,
PBMSwitchDimensions.[Status] as 'PBMSwitchDimensions_Status' ,
PBMSwitchPayer.[CheckingTime] as 'PBMSwitchPayer_Time' ,
PBMSwitchPayer.[Status] as 'PBMSwitchPayer_Status' ,
ShafafiyaValidate.[CheckingTime] as 'ShafafiyaValidate_Time' ,
ShafafiyaValidate.[Status] as 'ShafafiyaValidate_Status' 

 from ClinicianValidate  
 full outer join Claims on ClinicianValidate.rn = Claims.rn  
 full outer join Clinician on ClinicianValidate.rn = Clinician.rn  
 full outer join DHPOValidate on ClinicianValidate.rn = DHPOValidate.rn  
 full outer join Eauthorization on ClinicianValidate.rn = Eauthorization.rn  
 full outer join ERXValidate on ClinicianValidate.rn = ERXValidate.rn  
 full outer join ERXPharmacy on ClinicianValidate.rn = ERXPharmacy.rn  
 full outer join LMUValidate on ClinicianValidate.rn = LMUValidate.rn  
 full outer join MemberRegistration on ClinicianValidate.rn = MemberRegistration.rn  
 full outer join PBMLink on ClinicianValidate.rn = PBMLink.rn  
 full outer join PBMSwitchDimensions on ClinicianValidate.rn = PBMSwitchDimensions.rn  
 full outer join PBMSwitchPayer on ClinicianValidate.rn = PBMSwitchPayer.rn  
 full outer join ShafafiyaValidate on ClinicianValidate.rn = ShafafiyaValidate.rn     

  where CONVERT(date,ClinicianValidate.[CheckingTime]) > CONVERT(date,getdate()-2)  
