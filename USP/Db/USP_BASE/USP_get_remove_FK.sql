select 'alter table ' + object_name(A.parent_obj) + ' drop constraint ' + object_name(b.constid)
from sysobjects A  join sysforeignkeys B on A.id=B.constid
