insert into Sets select setid, name, CompletedSlots from TMog_Old..Sets

insert into Items select itemid, name, slot, null class, null subclass, null ilevel, null requiredlevel, null displayid, null flags, quality, buyprice, sellprice, acquired, hidden from TMog_Old..Items

insert into SetItems select setid, itemid from TMog_Old..Items

insert into Zones select * from TMog_Old..Zones where ZoneId = 210

insert into sources select type, description, subtype, null, wowheadid, ZoneId from TMog_Old..Sources where sourceid = 37