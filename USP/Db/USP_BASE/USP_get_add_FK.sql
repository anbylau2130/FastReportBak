select 'alter table ' + object_name(A.parent_obj) + ' add constraint ' + object_name(b.constid) + ' foreign key(' +  col_name(A.parent_obj,B.fkey) + ') references ' + object_name(B.rkeyid)  + '(' +  col_name(B.rkeyid,B.rkey) + ')'
from sysobjects A  join sysforeignkeys B on A.id=B.constid
