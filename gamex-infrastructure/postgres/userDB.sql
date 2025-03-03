DROP TABLE public."Users";

DROP DATABASE userdb;

SELECT  pg_terminate_backend(pid) FROM  pg_stat_activity WHERE
 datname = 'gplay_cms' AND  leader_pid IS NULL;
DROP DATABASE gplay_cms;

SELECT  pg_terminate_backend(pid) FROM  pg_stat_activity WHERE
 datname = 'test_userdb' AND  leader_pid IS NULL;
DROP DATABASE test_userdb;

SELECT  pg_terminate_backend(pid) FROM  pg_stat_activity WHERE
 datname = 'gplay_cms_test' AND  leader_pid IS NULL;
DROP DATABASE gplay_cms_test;

SELECT * FROM public."Users";
DELETE FROM public."Users";
DELETE FROM public."Users" WHERE Id = '5f9764f7-f2d2-46ad-b981-9b2ad206af8d';
DELETE FROM public."Users" WHERE "Email" = 'mayall@gakktechnology.com';

Insert into public."Users" ("Id", "FirstName","LastName", "Email", "UserName", "DateOfBirth", "Password", "CreatedById", "CreatedOn","UpdatedByID")
    Values('5f9764f7-f2d2-46ad-b981-9b2ad206af8e','Test', 'User', 'test@test.com', 'testuser', '2001-01-01', 'e10adc3949ba59abbe56e057f20f883e', '5f9764f7-f2d2-46ad-b981-9b2ad206af8d', Now(), '5f9764f7-f2d2-46ad-b981-9b2ad206af8d');



--- CMS
SELECT * FROM public."GameAttachments";
SELECT * FROM public."Genres";
SELECT * FROM public."GameSubscriptionTypes";
SELECT * FROM public."GameTechnologies";
SELECT * FROM public."GamePartners";
SELECT * FROM public."Tags";
SELECT * FROM public."Games";
SELECT * FROM public."GameTags";
SELECT * FROM public."GameSEODetail";
SELECT * FROM public."GameProperties";
SELECT * FROM public."GamePartnerUsers";
--DELETE FROM public."Games";

--UPDATE public."Games" SET "IsActive" = true WHERE  "Id" = 'be4ae42b-b8d1-43bc-b37a-c4344dad63e2';

SELECT "GamePartnerId", * FROM public."Games";
SELECT * FROM public."GamePartners" where "Id" = '7d3784a1-0026-4385-80ab-7a15c32a9731';
SELECT * FROM public."GamePartnerUsers" where  "UserId" = '5f9764f7-f2d2-46ad-b981-9b2ad206af8d';
SELECT * FROM public."Users";


CREATE USER sonarqube WITH PASSWORD 'Xu5#R0Tk1BzP';

select   p."FirstName", p."LastName", p."Email", p."Mobile", "IpAddress", "UserAgent", "DeviceInfo", "Location"
from public."UserLogActivities" as la 
inner join public."Users" as  u on la."UserId" = u."Id"
inner join public."Players" as p on u."PlayerId" = p."Id"
order by la."CreatedOn" desc
LIMIT 1000;


select    la.*
from public.user_log_activities as la 
left join public.users as  u on la.user_id = u.id
left join public.players as p on u.player_id = p.id
order by la.created_on desc
LIMIT 1000;



select * from public."UserLogActivities" order by "CreatedOn" desc


SELECT * FROM public."Players";

Insert into public."Players" ("Id", "FirstName", "LastName", "Email", "IsActive", "CreatedById", "CreatedOn", "RegistrationSourceId", "UpdatedById")
Values('5f9764f7-f2d2-46ad-b981-9b2ad206af8d','GP', 'Team', 'gpteam@gmail.com', true, '7d3784a1-0026-4385-80ab-7a15c32a9731',Now(), 1, '7d3784a1-0026-4385-80ab-7a15c32a9731');

Insert into public."Users" ("Id", "PlayerId", "UserName", "Password", "IsActive", "CreatedById", "CreatedOn", "UpdatedById")
Values('5f9764f7-f2d2-46ad-b981-9b2ad206af8d','5f9764f7-f2d2-46ad-b981-9b2ad206af8d','GPTeam', '8b855eed40b4dffc4543d755b0ef5a90', true, '7d3784a1-0026-4385-80ab-7a15c32a9731',Now(), '7d3784a1-0026-4385-80ab-7a15c32a9731');


