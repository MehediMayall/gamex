INSERT INTO public.players
(id,first_name,last_name,email,mobile,date_of_birth,city,country,avatar_name,registration_source_id,is_active,created_by_id,
created_on,updated_on,deleted_on,updated_by_id)

SELECT "Id","FirstName","LastName","Email","Mobile","DateOfBirth","City","Country","AvatarName","RegistrationSourceId","IsActive","CreatedById","CreatedOn","UpdatedOn","DeletedOn","UpdatedById" 
FROM dblink('host=192.168.101.14 dbname=gamex_players_old user=gamexDev password=Un*X8#acR0Tk1BzP',
    'select "Id","FirstName","LastName","Email","Mobile","DateOfBirth","City","Country","AvatarName","RegistrationSourceId","IsActive","CreatedById","CreatedOn","UpdatedOn","DeletedOn","UpdatedById" from public."Players"')
AS t1(
    "Id" uuid,
    "FirstName" text,
    "LastName" text,
    "Email" text,
    "Mobile" text,
    "DateOfBirth" date,
    "City" text,
    "Country" text,
    "AvatarName" text,
    "RegistrationSourceId" integer,
    "IsActive" boolean,
    "CreatedById" uuid,
    "CreatedOn" timestamp with time zone,
    "UpdatedOn" timestamp with time zone,
    "DeletedOn" timestamp with time zone,
    "UpdatedById" uuid
);



INSERT INTO public.users (id,player_id,user_name,password,is_active,created_by_id,created_on,updated_on,deleted_on,updated_by_id)

SELECT "Id","PlayerId","UserName","Password","IsActive","CreatedById","CreatedOn","UpdatedOn","DeletedOn","UpdatedById" FROM dblink('host=192.168.101.14 dbname=gamex_players_old user=gamexDev password=Un*X8#acR0Tk1BzP',
    'select   "Id","PlayerId","UserName","Password","IsActive","CreatedById","CreatedOn","UpdatedOn","DeletedOn","UpdatedById" from public."Users"')
AS t1(
    "Id" uuid ,
    "PlayerId" uuid ,
    "UserName" text ,
    "Password" text ,
    "IsActive" boolean,
    "CreatedById" uuid ,
    "CreatedOn" timestamp with time zone ,
    "UpdatedOn" timestamp with time zone,
    "DeletedOn" timestamp with time zone,
    "UpdatedById" uuid
);

