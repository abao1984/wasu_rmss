/*exec sp_configure 'show advanced options',1
reconfigure
exec sp_configure 'Ad Hoc Distributed Queries',1
reconfigure
*/
use rmss
go
create view ClientTypeA as
select * 
from openrowset('OraOLEDB.Oracle','orcl';'hsyw';'hsyw','select * from rmss')