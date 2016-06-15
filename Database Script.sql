--Gedurende de inserts ben ik een paar fouten tegen gekomen die ik er uithebgehaald

--drop table onderwerp cascade constraints;

--alter table mappy
--add ONDERWERP varchar2(20);

--alter table mappy
--add constraint ONDERWERP_CHECK CHECK(ONDERWERP in('Games','It-pro','Computers'));



--alter table mappy 
--drop column onderwerp_id;
INSERT INTO ACCA
VALUES(1,'Mitch',sysdate, 'M', 'Uden', 'Fontys', 'ZyXeMaster',sysdate,sysdate,'password7');
INSERT INTO ACCA
VALUES(2,'Thijs',sysdate, 'M', 'Uden', 'Fontys', 'Tyrmius',sysdate,sysdate,'password7');
INSERT INTO ACCA
VALUES(3,'Sam',sysdate, 'F', 'Uden', 'Fontys', 'Sweetpallyd',sysdate,sysdate,'password7');
INSERT INTO ACCA
VALUES(4,'Paul',sysdate, 'M', 'Uden', 'Fontys', 'Nightslayer',sysdate,sysdate,'password7');
INSERT INTO ACCA
VALUES(5,'Wies',sysdate, 'M', 'Uden', 'Fontys', 'MisterCrazyBoy21',sysdate,sysdate,'password7');

--De onderwerpen van de forums, de onderwerp table is weggehaalt omdat die onnodig is
INSERT INTO MAPPY
VALUES(1,'Onderdelen','COMPUTERS');
INSERT INTO MAPPY
VALUES(2,'Performance','COMPUTERS');
INSERT INTO MAPPY
VALUES(3,'Nieuw','GAMES');
INSERT INTO MAPPY
VALUES(4,'Aanbevelingen','GAMES');
INSERT INTO MAPPY
VALUES(5,'How-to','IT-PRO');

-- Van alle 3 categoriën een insertion
INSERT INTO PRODUCT
VALUES(1,'Darksouls', 'GAME','Fake.nl');
INSERT INTO PRODUCT
VALUES(2,'Spore', 'GAME','Fake.nl');
INSERT INTO PRODUCT
VALUES(3,'Ps4', 'CONSOLE','Fake.nl');
INSERT INTO PRODUCT
VALUES(4,'Xbox1', 'CONSOLE','Fake.nl');
INSERT INTO PRODUCT
VALUES(5,'SamsungX444', 'TV','Fake.nl');

--De bijbehoordende specs van de producten
INSERT INTO PRODUCT_GAMESPEC
VALUES(1,1,'playstation', 'fantasy','ZyxemInc',18);
INSERT INTO PRODUCT_GAMESPEC
VALUES(2,2,'Xbox', 'fantasy','ZyxemInc',12);
INSERT INTO PRoDUCT_CONSOLESPEC
VALUES(3,3,'Zwart', 'niets','2', '500mb');
INSERT INTO PRODUCT_CONSOLESPEC
VALUES(4,4,'Wit', 'Waterdicht','5','1T');
INSERT INTO PRODUCT_TVSPEC
VALUES(5,5,'5', 'Full-HD', '60hz',null);

--Verschillende uitgevers
INSERT INTO UITGEVER
VALUES(1,'KoopSlim');
INSERT INTO UITGEVER
VALUES(2,'Alternate');
INSERT INTO UITGEVER
VALUES(3,'MeckGegkko');
INSERT INTO UITGEVER
VALUES(4,'BartSmit');
INSERT INTO UITGEVER
VALUES(5,'Intertoys');

--Een enkele wenslijst waar meerdere producten in gaan
INSERT INTO WENSLIJST
VALUES(1,1,'Superlijst');

--Alle producten van de wenslijst met hoeveelheden
INSERT INTO WENSLIJST_PRODUCT
VALUES(1,1,1);
INSERT INTO WENSLIJST_PRODUCT
VALUES(1,2,1);
INSERT INTO WENSLIJST_PRODUCT
VALUES(1,3,1);
INSERT INTO WENSLIJST_PRODUCT
VALUES(1,4,2);
INSERT INTO WENSLIJST_PRODUCT
VALUES(1,5,3);

--Uigever gegevens gelinkt met de producten/ dit zijn de daadwerkelijke producten te koop
INSERT INTO PRODUCT_LINK
VALUES(1,1,20,'RANDOM.NL',7);
INSERT INTO PRODUCT_LINK
VALUES(2,2,15,'RANDOM.NL',7);
INSERT INTO PRODUCT_LINK
VALUES(3,3,12.50,'RANDOM.NL',7);
INSERT INTO PRODUCT_LINK
VALUES(4,4,33,'RANDOM.NL',7);
INSERT INTO PRODUCT_LINK
VALUES(5,5,20,'RANDOM.NL',7);


--drop table acca_review cascade constraints;
--drop table Uitgever_review cascade constraints;



--alter table uitgever_acca_review
--drop constraint soort_review;

--alter table uitgever_acca_review
--add constraint SOORT_REVIEW CHECK(SOORT IN('A','U'));

--De gecombineerde subtypering met de bijbehoordende null waardes
insert into uitgever_acca_review
values(1,1,2,null,'Dit is een review', 6,6,4,null,null,null, 'A');
insert into uitgever_acca_review
values(2,1,3,null,'Dit is een review', 6,7,6,null,null,null, 'A');
insert into uitgever_acca_review
values(3,1,4,null,'Dit is een review', 2,9,6,null,null,null, 'A');
insert into uitgever_acca_review
values(4,2,null,1,'Dit is een review', null,null,null,6,6,6, 'U');
insert into uitgever_acca_review
values(5,2,null,2,'Dit is een review', null,null,null,6,6,2, 'U');

--alter table post 
--add constraint Zelf_check CHECK(POST_ID <> ID);

--Posts over de producten en reacties
insert into post 
values(1,'Wat moet ik kopen?',1, 'Advies', null,1);
insert into post 
values(2,'Wat is sneller Xbox of ps4?',1, 'KoopDillemma', null,2);
insert into post 
values(3,'Wat zijn leuke games dit jaar?',2, 'Games', null,4);
insert into post 
values(4,'Darksouls 3 natuurlijk',5, 'Games', 3,4);
insert into post 
values(5,'Xbox wantja Microsoft!',5, 'KoopDillemma', 2,2);


--De upvotes van de posts 
insert into upvote
values(1,4);
insert into upvote
values(2,4);
insert into upvote
values(3,4);
insert into upvote
values(4,4);
insert into upvote
values(5,2);

--Een willekeurig gesprek ook met 2 antwoorden op 1 bericht
insert into BERICHT
values(1,'Hey <3', 'Gegroet',1,4,null);
insert into BERICHT
values(2,'Hallo', 'Gegroet',4,1,1);
insert into BERICHT
values(3,'Ik ben verlegen', 'Gegroet',4,1,1);
insert into BERICHT
values(4,'Dit is relevant', 'Hallo',5,3,null);
insert into BERICHT
values(5,'Niet waar', 'Hallo',3,5,4);

--In het lijstje om te vergelijken per product
insert into vergelijking
values(1,2);
insert into vergelijking
values(1,3);
insert into vergelijking
values(1,1);
insert into vergelijking
values(4,2);
insert into vergelijking
values(3,1);

--alter table product_review
--add  Text varchar2(500);

--Reviews over de producten met algemene informatie
insert into product_review
values(1,1,1,'Alles-dit-dat','Niets', 7,'Heel goed');
insert into product_review
values(2,2,1,'Alles-dit-dat','Niets', 7, 'Echt geweldig');
insert into product_review
values(3,4,1,'Alles-dit-dat','dit-dat', 7, 'Slecht voor story');
insert into product_review
values(4,1,3,'Alles-dit-dat','Niets', 7,'Matig');
insert into product_review
values(5,1,4,'Alles-dit-dat','Niets', 7,'Redelijk');

--De subtypering van de product reviews met alle individuele informatie
insert into review_GAMECRIT
values(1,1,9,9,9,9,9,9);
insert into review_GAMECRIT
values(2,2,5,6,8,7,6,9);
insert into review_GAMECRIT
values(3,3,3,5,6,2,7,8);
insert into review_CONSOLECRIT
values(4,4,5,8,6,4,8);
insert into review_CONSOLECRIT
values(5,5,5,3,7,3,8);






