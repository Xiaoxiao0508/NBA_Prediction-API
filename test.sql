select name 
from sys.databases
use [nba-db-main]
select *
from Player
SELECT table_catalog[database],table_schema [schema],table_name name,table_type type
FROM INFORMATION_SCHEMA.TABLES
GO

select * 
from Team
select *
from Player
where FIRSTNAME LIKE 'L%'

