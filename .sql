select name 
from sys.databases

use [NBA_Version2]

SELECT table_catalog[database],table_schema [schema],table_name name,table_type type
FROM INFORMATION_SCHEMA.TABLES
GO

SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE
 FROM INFORMATION_SCHEMA. COLUMNS 
 WHERE TABLE_NAME='AspNetUsers'



 
 select * from Team
select * from allPlayers
select * from altAllPlayers
select *from PlayerSelection

select * from AspNetUsers
select *from AspNetUserTokens
select *from AspNetRoles
select* from AspNetRoleClaims
select * from AspNetUserLogins
EXEC DtrScores @userId='aabe87eb-9e11-45b2-acbd-6ac8b7311ed6';
