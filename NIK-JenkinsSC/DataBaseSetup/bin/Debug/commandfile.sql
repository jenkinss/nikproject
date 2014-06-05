USE [master]
GO
CREATE DATABASE [WTdatabase] ON 
( FILENAME = N'C:\VS_workspace\SoyalWorkTimeWebManager\App_Data\aspnet-SoyalWorkTimeWebManager-20140402091553.mdf' ),
( FILENAME = N'C:\VS_workspace\SoyalWorkTimeWebManager\App_Data\aspnet-SoyalWorkTimeWebManager-20140402091553_log.ldf' )
 FOR ATTACH ;
GO
exit;
quit;