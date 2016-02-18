using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeltyAutomation.M2M.QueryData;

namespace ClearstreamWeb.Models {
    public interface ITimeKeepingRepository {
        IQueryable<WacsTimeKeepingProject> Projects { get; }
        WacsTimeKeepingProject AddProject(WacsTimeKeepingProject newProject);
        bool UpdateProject(WacsTimeKeepingProject updatedProject);
        bool DeleteProject(int id);

        IQueryable<WacsTimeKeepingUser> Users { get; }
        WacsTimeKeepingUser GetUserByName(string login);
        WacsTimeKeepingUser GetAdminUserByName(string login);
        WacsTimeKeepingUser AddUser(WacsTimeKeepingUser newUser);
        bool UpdateUser(WacsTimeKeepingUser updatedUser);
        bool DeleteUser(int id);

        IQueryable<WacsTimeKeepingEntry> Entries { get; }
        WacsTimeKeepingEntry AddEntry(WacsTimeKeepingEntry newEntry);
        bool UpdateEntry(WacsTimeKeepingEntry updatedEntry);
        bool DeleteEntry(int id);
    }

    public class TestTimeKeepingRepository : ITimeKeepingRepository {
        private static TestTimeKeepingRepository instance;
        private List<WacsTimeKeepingUser> users;
        private List<WacsTimeKeepingProject> projects;
        private List<WacsTimeKeepingEntry> entries;
        private List<WacsTimeKeepingDepartment> departments;

        private TestTimeKeepingRepository() {
            initialize();
        }

        public static TestTimeKeepingRepository Instance {
            get {
                if (instance == null) {
                    instance = new TestTimeKeepingRepository();
                }
                return instance;
            }
        }

        private void initialize() {
            departments = new List<WacsTimeKeepingDepartment> {
                new WacsTimeKeepingDepartment {
                    Id = 1,
                    Name = "Engineering"
                }
            };

            users = JsonConvert.DeserializeObject<List<WacsTimeKeepingUser>>(@"[{ 'id':6,'login':'MESTEKCORP\\io_rbaker','displayName':'Bob','departmentId':1,'isDepartmentAdmin':false},{ 'id':12,'login':'MESTEKCORP\\io_dsawyer','displayName':'Dave','departmentId':1,'isDepartmentAdmin':false},{ 'id':9,'login':'MESTEKCORP\\io_edunkel','displayName':'Eddie','departmentId':1,'isDepartmentAdmin':false},{ 'id':16,'login':'MESTEKCORP\\io_jwilson','displayName':'Jim ','departmentId':1,'isDepartmentAdmin':true},{ 'id':14,'login':'MESTEKCORP\\io_jwelty','displayName':'John W','departmentId':1,'isDepartmentAdmin':true},{ 'id':13,'login':'MESTEKCORP\\io_jzlatohlavek','displayName':'John Z','departmentId':1,'isDepartmentAdmin':false},{ 'id':15,'login':'MESTEKCORP\\io_jsbrown','displayName':'Judy','departmentId':1,'isDepartmentAdmin':false},{ 'id':5,'login':'MESTEKCORP\\io_lrieck','displayName':'Lee','departmentId':1,'isDepartmentAdmin':false},{ 'id':11,'login':'MESTEKCORP\\io_mberry','displayName':'Megan','departmentId':1,'isDepartmentAdmin':false},{ 'id':10,'login':'MESTEKCORP\\io_skapoor','displayName':'Sid','departmentId':1,'isDepartmentAdmin':false},{ 'id':8,'login':'MESTEKCORP\\io_sfuegen','displayName':'Steve Fuegen','departmentId':1,'isDepartmentAdmin':false},{ 'id':7,'login':'MESTEKCORP\\io_sfunk','displayName':'Steve Funk','departmentId':1,'isDepartmentAdmin':false},{ 'id':1,'login':'MESTEKCORP\\io_skerska','displayName':'Steve K','departmentId':1,'isDepartmentAdmin':true},{ 'id':4,'login':'MESTEKCORP\\io_tcoon','displayName':'Tim','departmentId':1,'isDepartmentAdmin':false}]");

            projects = JsonConvert.DeserializeObject<List<WacsTimeKeepingProject>>(@"[{'id':40,'createdByUserId':1,'saleNo':'','description':'AFTERMARKET','active':true,'openingHours':0.00,'formattedDescription':'AFTERMARKET'},{'id':6,'createdByUserId':1,'saleNo':'','description':'AHR SHOW','active':true,'openingHours':0.00,'formattedDescription':'AHR SHOW'},{'id':36,'createdByUserId':1,'saleNo':'055490','description':'AIR DUCT PRODUCTS','active':true,'openingHours':76.00,'formattedDescription':'055490: AIR DUCT PRODUCTS'},{'id':14,'createdByUserId':1,'saleNo':'055750','description':'AMX MECHANICAL','active':true,'openingHours':22.00,'formattedDescription':'055750: AMX MECHANICAL'},{'id':7,'createdByUserId':1,'saleNo':'051370','description':'CLARCOR','active':true,'openingHours':510.75,'formattedDescription':'051370: CLARCOR'},{'id':18,'createdByUserId':1,'saleNo':'056448','description':'CONDITIONED AIR SYSTEMS ','active':true,'openingHours':0.00,'formattedDescription':'056448: CONDITIONED AIR SYSTEMS'},{'id':22,'createdByUserId':1,'saleNo':'056640','description':'CONDITIONED AIR SYSTEMS','active':true,'openingHours':0.00,'formattedDescription':'056640: CONDITIONED AIR SYSTEMS'},{'id':28,'createdByUserId':1,'saleNo':'056455','description':'CREATIVE METAL SOLUTIONS','active':true,'openingHours':0.00,'formattedDescription':'056455: CREATIVE METAL SOLUTIONS'},{'id':43,'createdByUserId':1,'saleNo':'','description':'DRAWING CONVERSIONS','active':true,'openingHours':0.00,'formattedDescription':'DRAWING CONVERSIONS'},{'id':4,'createdByUserId':1,'saleNo':'050569','description':'EATON','active':true,'openingHours':549.50,'formattedDescription':'050569: EATON'},{'id':30,'createdByUserId':1,'saleNo':'056654','description':'ECKER MECHANICAL','active':true,'openingHours':0.00,'formattedDescription':'056654: ECKER MECHANICAL'},{'id':20,'createdByUserId':1,'saleNo':'055102','description':'ENVIRONMENTAL AIR','active':true,'openingHours':0.00,'formattedDescription':'055102: ENVIRONMENTAL AIR'},{'id':54,'createdByUserId':1,'saleNo':'056474','description':'Erhardt','active':true,'openingHours':0.00,'formattedDescription':'056474: Erhardt'},{'id':25,'createdByUserId':1,'saleNo':'056313','description':'FORMTEK INT','active':true,'openingHours':0.00,'formattedDescription':'056313: FORMTEK INT'},{'id':27,'createdByUserId':1,'saleNo':'056542','description':'FORMTEK INTL','active':true,'openingHours':0.00,'formattedDescription':'056542: FORMTEK INTL'},{'id':17,'createdByUserId':1,'saleNo':'055678','description':'FORMTEK INTL','active':true,'openingHours':0.00,'formattedDescription':'055678: FORMTEK INTL'},{'id':24,'createdByUserId':1,'saleNo':'056320','description':'H&H HEATING & COOLING','active':true,'openingHours':0.00,'formattedDescription':'056320: H&H HEATING & COOLING'},{'id':37,'createdByUserId':1,'saleNo':'055414','description':'HART & COOLEY','active':true,'openingHours':61.00,'formattedDescription':'055414: HART & COOLEY'},{'id':34,'createdByUserId':1,'saleNo':'046685','description':'HART & COOLEY','active':true,'openingHours':1776.30,'formattedDescription':'046685: HART & COOLEY'},{'id':2,'createdByUserId':1,'saleNo':'053116','description':'HART & COOLEY','active':true,'openingHours':0.00,'formattedDescription':'053116: HART & COOLEY'},{'id':55,'createdByUserId':1,'saleNo':'056894','description':'HERCULES','active':true,'openingHours':0.00,'formattedDescription':'056894: HERCULES'},{'id':47,'createdByUserId':1,'saleNo':'','description':'HOLIDAY','active':true,'openingHours':0.00,'formattedDescription':'HOLIDAY'},{'id':3,'createdByUserId':1,'saleNo':'048801','description':'HTP','active':true,'openingHours':745.00,'formattedDescription':'048801: HTP'},{'id':39,'createdByUserId':1,'saleNo':'053561','description':'IMG','active':true,'openingHours':34.00,'formattedDescription':'053561: IMG'},{'id':23,'createdByUserId':1,'saleNo':'056374','description':'INLAND METAL','active':true,'openingHours':0.00,'formattedDescription':'056374: INLAND METAL'},{'id':48,'createdByUserId':1,'saleNo':'','description':'INSTALL','active':true,'openingHours':0.00,'formattedDescription':'INSTALL'},{'id':41,'createdByUserId':1,'saleNo':'','description':'ISO','active':true,'openingHours':0.00,'formattedDescription':'ISO'},{'id':35,'createdByUserId':1,'saleNo':'056505','description':'JM BRENNAN','active':true,'openingHours':0.00,'formattedDescription':'056505: JM BRENNAN'},{'id':32,'createdByUserId':1,'saleNo':'056539','description':'KNIGHT HTG & AC','active':true,'openingHours':0.00,'formattedDescription':'056539: KNIGHT HTG & AC'},{'id':49,'createdByUserId':1,'saleNo':'','description':'MANUALS','active':true,'openingHours':0.00,'formattedDescription':'MANUALS'},{'id':21,'createdByUserId':1,'saleNo':'056369','description':'M-TECH','active':true,'openingHours':0.00,'formattedDescription':'056369: M-TECH'},{'id':46,'createdByUserId':1,'saleNo':'','description':'PERSONAL DAY','active':true,'openingHours':0.00,'formattedDescription':'PERSONAL DAY'},{'id':33,'createdByUserId':1,'saleNo':'055317','description':'PRO-FAB','active':true,'openingHours':44.00,'formattedDescription':'055317: PRO-FAB'},{'id':50,'createdByUserId':1,'saleNo':'','description':'PROJECT STATUS MEETING','active':true,'openingHours':0.00,'formattedDescription':'PROJECT STATUS MEETING'},{'id':51,'createdByUserId':1,'saleNo':'','description':'QUALITY CONTROL','active':true,'openingHours':0.00,'formattedDescription':'QUALITY CONTROL'},{'id':52,'createdByUserId':1,'saleNo':'','description':'ROLLFORMERS','active':true,'openingHours':0.00,'formattedDescription':'ROLLFORMERS'},{'id':12,'createdByUserId':1,'saleNo':'','description':'SALES','active':true,'openingHours':0.00,'formattedDescription':'SALES'},{'id':15,'createdByUserId':1,'saleNo':'053806','description':'SELKIRK ELBOW MACH','active':true,'openingHours':115.00,'formattedDescription':'053806: SELKIRK ELBOW MACH'},{'id':11,'createdByUserId':1,'saleNo':'','description':'SERVICE','active':true,'openingHours':0.00,'formattedDescription':'SERVICE'},{'id':42,'createdByUserId':1,'saleNo':'','description':'SHOP ASSISTANCE','active':true,'openingHours':0.00,'formattedDescription':'SHOP ASSISTANCE'},{'id':38,'createdByUserId':1,'saleNo':'055089','description':'SLAGLE MECHANICAL','active':true,'openingHours':29.00,'formattedDescription':'055089: SLAGLE MECHANICAL'},{'id':29,'createdByUserId':1,'saleNo':'056234','description':'SOUTHWARK METAL','active':true,'openingHours':0.00,'formattedDescription':'056234: SOUTHWARK METAL'},{'id':16,'createdByUserId':1,'saleNo':'052618','description':'THERMOKOOL','active':true,'openingHours':139.00,'formattedDescription':'052618: THERMOKOOL'},{'id':44,'createdByUserId':1,'saleNo':'','description':'TRAINING','active':true,'openingHours':0.00,'formattedDescription':'TRAINING'},{'id':31,'createdByUserId':1,'saleNo':'056415','description':'UNITED HVAC','active':true,'openingHours':0.00,'formattedDescription':'056415: UNITED HVAC'},{'id':5,'createdByUserId':1,'saleNo':'055073','description':'UTILITY TRAILER','active':true,'openingHours':6.00,'formattedDescription':'055073: UTILITY TRAILER'},{'id':45,'createdByUserId':1,'saleNo':'','description':'VACATION','active':true,'openingHours':0.00,'formattedDescription':'VACATION'},{'id':53,'createdByUserId':1,'saleNo':'','description':'WARRANTY','active':true,'openingHours':0.00,'formattedDescription':'WARRANTY'},{'id':87,'createdByUserId':1,'saleNo':'051014','description':'Acosta SM 456','active':false,'openingHours':50.00,'formattedDescription':'051014: Acosta SM 456'},{'id':86,'createdByUserId':1,'saleNo':'050573','description':'Acosta SM 916','active':false,'openingHours':40.00,'formattedDescription':'050573: Acosta SM 916'},{'id':61,'createdByUserId':1,'saleNo':'053719','description':'Air Purchases','active':false,'openingHours':10.00,'formattedDescription':'053719: Air Purchases'},{'id':105,'createdByUserId':1,'saleNo':'047399','description':'Ameristeel Stacker','active':false,'openingHours':22.00,'formattedDescription':'047399: Ameristeel Stacker'},{'id':83,'createdByUserId':1,'saleNo':'049700','description':'ATCO Gary Rebuild','active':false,'openingHours':7.00,'formattedDescription':'049700: ATCO Gary Rebuild'},{'id':74,'createdByUserId':1,'saleNo':'054294','description':'Baker Group','active':false,'openingHours':23.00,'formattedDescription':'054294: Baker Group'},{'id':56,'createdByUserId':1,'saleNo':'053688','description':'Calefaccion','active':false,'openingHours':32.00,'formattedDescription':'053688: Calefaccion'},{'id':72,'createdByUserId':1,'saleNo':'051173','description':'Capital Hardware 678&916','active':false,'openingHours':147.00,'formattedDescription':'051173: Capital Hardware 678&916'},{'id':108,'createdByUserId':1,'saleNo':'047559','description':'CHI','active':false,'openingHours':91.00,'formattedDescription':'047559: CHI'},{'id':95,'createdByUserId':1,'saleNo':'049616','description':'Comfort Group','active':false,'openingHours':35.00,'formattedDescription':'049616: Comfort Group'},{'id':77,'createdByUserId':1,'saleNo':'052552','description':'Commercial Duct','active':false,'openingHours':35.00,'formattedDescription':'052552: Commercial Duct'},{'id':102,'createdByUserId':1,'saleNo':'049219','description':'Custom Sheet Metal Controls','active':false,'openingHours':23.00,'formattedDescription':'049219: Custom Sheet Metal Controls'},{'id':58,'createdByUserId':1,'saleNo':'054043','description':'Danforth','active':false,'openingHours':36.00,'formattedDescription':'054043: Danforth'},{'id':101,'createdByUserId':1,'saleNo':'048872','description':'Dixon Heating','active':false,'openingHours':61.00,'formattedDescription':'048872: Dixon Heating'},{'id':68,'createdByUserId':1,'saleNo':'051747','description':'Durasystems','active':false,'openingHours':131.00,'formattedDescription':'051747: Durasystems'},{'id':78,'createdByUserId':1,'saleNo':'052607','description':'Fidelity Engineering','active':false,'openingHours':37.00,'formattedDescription':'052607: Fidelity Engineering'},{'id':84,'createdByUserId':1,'saleNo':'048793','description':'G&S Metal','active':false,'openingHours':119.00,'formattedDescription':'048793: G&S Metal'},{'id':80,'createdByUserId':1,'saleNo':'050894','description':'Haggerty','active':false,'openingHours':68.00,'formattedDescription':'050894: Haggerty'},{'id':91,'createdByUserId':1,'saleNo':'051090','description':'Handy Versa','active':false,'openingHours':9.00,'formattedDescription':'051090: Handy Versa'},{'id':82,'createdByUserId':1,'saleNo':'052217','description':'Hercules','active':false,'openingHours':17.00,'formattedDescription':'052217: Hercules'},{'id':88,'createdByUserId':1,'saleNo':'050116','description':'Hercules AEM Rebuild','active':false,'openingHours':24.00,'formattedDescription':'050116: Hercules AEM Rebuild'},{'id':70,'createdByUserId':1,'saleNo':'055108','description':'Heritage Seam Closer','active':false,'openingHours':22.00,'formattedDescription':'055108: Heritage Seam Closer'},{'id':69,'createdByUserId':1,'saleNo':'054802','description':'Herman Reeves','active':false,'openingHours':5.00,'formattedDescription':'054802: Herman Reeves'},{'id':93,'createdByUserId':1,'saleNo':'048354','description':'IMG - Piperoller','active':false,'openingHours':109.50,'formattedDescription':'048354: IMG - Piperoller'},{'id':62,'createdByUserId':1,'saleNo':'054576','description':'IMG Collar Maker','active':false,'openingHours':15.00,'formattedDescription':'054576: IMG Collar Maker'},{'id':92,'createdByUserId':1,'saleNo':'047163','description':'Imperial - Piperoller','active':false,'openingHours':169.00,'formattedDescription':'047163: Imperial - Piperoller'},{'id':59,'createdByUserId':1,'saleNo':'054383','description':'JIT Steel Service','active':false,'openingHours':23.00,'formattedDescription':'054383: JIT Steel Service'},{'id':104,'createdByUserId':1,'saleNo':'049636','description':'Kruck','active':false,'openingHours':14.00,'formattedDescription':'049636: Kruck'},{'id':76,'createdByUserId':1,'saleNo':'051211','description':'McCusker Gill','active':false,'openingHours':139.00,'formattedDescription':'051211: McCusker Gill'},{'id':94,'createdByUserId':1,'saleNo':'050032','description':'McDonald Miller Controls','active':false,'openingHours':110.00,'formattedDescription':'050032: McDonald Miller Controls'},{'id':100,'createdByUserId':1,'saleNo':'049761','description':'Mechanical Air','active':false,'openingHours':6.00,'formattedDescription':'049761: Mechanical Air'},{'id':79,'createdByUserId':1,'saleNo':'049991','description':'Mechanical Trades','active':false,'openingHours':27.00,'formattedDescription':'049991: Mechanical Trades'},{'id':107,'createdByUserId':1,'saleNo':'048286','description':'Metal Air PCF','active':false,'openingHours':7.00,'formattedDescription':'048286: Metal Air PCF'},{'id':98,'createdByUserId':1,'saleNo':'049045','description':'Midstates - ProFabriduct','active':false,'openingHours':165.00,'formattedDescription':'049045: Midstates - ProFabriduct'},{'id':99,'createdByUserId':1,'saleNo':'048273','description':'Miller Metals - Controls Package','active':false,'openingHours':88.00,'formattedDescription':'048273: Miller Metals - Controls Package'},{'id':63,'createdByUserId':1,'saleNo':'055333','description':'Pacific Duct','active':false,'openingHours':10.00,'formattedDescription':'055333: Pacific Duct'},{'id':60,'createdByUserId':1,'saleNo':'053395','description':'Phoenix Metals','active':false,'openingHours':70.00,'formattedDescription':'053395: Phoenix Metals'},{'id':103,'createdByUserId':1,'saleNo':'049209','description':'Ranco Controls','active':false,'openingHours':9.00,'formattedDescription':'049209: Ranco Controls'},{'id':85,'createdByUserId':1,'saleNo':'051080','description':'Regional SM Knife','active':false,'openingHours':7.00,'formattedDescription':'051080: Regional SM Knife'},{'id':96,'createdByUserId':1,'saleNo':'047291','description':'Selkirk Elbow Machines','active':false,'openingHours':53.00,'formattedDescription':'047291: Selkirk Elbow Machines'},{'id':73,'createdByUserId':1,'saleNo':'053711','description':'Sheet Metal Werks','active':false,'openingHours':22.00,'formattedDescription':'053711: Sheet Metal Werks'},{'id':66,'createdByUserId':1,'saleNo':'055126','description':'Smith\'s','active':false,'openingHours':24.00,'formattedDescription':'055126: Smith\'s'},{'id':106,'createdByUserId':1,'saleNo':'048388','description':'Southport H&C','active':false,'openingHours':48.00,'formattedDescription':'048388: Southport H&C'},{'id':65,'createdByUserId':1,'saleNo':'055296','description':'Southwark','active':false,'openingHours':67.00,'formattedDescription':'055296: Southwark'},{'id':71,'createdByUserId':1,'saleNo':'049693','description':'Steelcase','active':false,'openingHours':523.00,'formattedDescription':'049693: Steelcase'},{'id':97,'createdByUserId':1,'saleNo':'048052','description':'Stoughton Trailer - Peeler table','active':false,'openingHours':36.00,'formattedDescription':'048052: Stoughton Trailer - Peeler table'},{'id':89,'createdByUserId':1,'saleNo':'046527','description':'Utility Trailer Panel Line','active':false,'openingHours':1751.25,'formattedDescription':'046527: Utility Trailer Panel Line'},{'id':57,'createdByUserId':1,'saleNo':'052859','description':'Vale Ovaler','active':false,'openingHours':22.00,'formattedDescription':'052859: Vale Ovaler'},{'id':67,'createdByUserId':1,'saleNo':'048800','description':'Velux Elbow','active':false,'openingHours':282.00,'formattedDescription':'048800: Velux Elbow'},{'id':90,'createdByUserId':1,'saleNo':'047385','description':'Werner','active':false,'openingHours':371.50,'formattedDescription':'047385: Werner'},{'id':81,'createdByUserId':1,'saleNo':'051691','description':'Wichita SM Controls','active':false,'openingHours':17.00,'formattedDescription':'051691: Wichita SM Controls'},{'id':75,'createdByUserId':1,'saleNo':'051697','description':'Wichita SM Tubematic','active':false,'openingHours':27.00,'formattedDescription':'051697: Wichita SM Tubematic'},{'id':64,'createdByUserId':1,'saleNo':'054910','description':'Woodall Roofing','active':false,'openingHours':18.00,'formattedDescription':'054910: Woodall Roofing'}]");

            entries = JsonConvert.DeserializeObject<List<WacsTimeKeepingEntry>>(@"[{'id':133,'userId':14,'projectId':11,'started':'2016-01-04T12:00:00+00:00','ended':'2016-01-04T12:30:00+00:00'},{'id':135,'userId':14,'projectId':11,'started':'2016-01-05T12:00:00+00:00','ended':'2016-01-05T15:00:00+00:00'},{'id':132,'userId':14,'projectId':11,'started':'2016-01-06T12:00:00+00:00','ended':'2016-01-06T15:00:00+00:00'},{'id':141,'userId':14,'projectId':11,'started':'2016-01-07T12:00:00+00:00','ended':'2016-01-07T12:30:00+00:00'},{'id':128,'userId':14,'projectId':11,'started':'2016-01-11T12:00:00+00:00','ended':'2016-01-11T20:30:00+00:00'},{'id':583,'userId':14,'projectId':11,'started':'2016-01-12T12:00:00+00:00','ended':'2016-01-12T21:15:00+00:00'},{'id':575,'userId':14,'projectId':11,'started':'2016-01-13T12:00:00+00:00','ended':'2016-01-13T20:50:00+00:00'},{'id':585,'userId':14,'projectId':11,'started':'2016-01-15T12:00:00+00:00','ended':'2016-01-15T13:43:00+00:00'},{'id':967,'userId':14,'projectId':3,'started':'2016-01-18T12:00:00+00:00','ended':'2016-01-18T12:45:00+00:00'},{'id':968,'userId':14,'projectId':3,'started':'2016-01-19T12:00:00+00:00','ended':'2016-01-19T16:30:00+00:00'},{'id':969,'userId':14,'projectId':11,'started':'2016-01-19T16:30:00+00:00','ended':'2016-01-19T17:00:00+00:00'},{'id':970,'userId':14,'projectId':3,'started':'2016-01-20T12:00:00+00:00','ended':'2016-01-20T17:30:00+00:00'},{'id':971,'userId':14,'projectId':11,'started':'2016-01-20T17:30:00+00:00','ended':'2016-01-20T18:45:00+00:00'},{'id':972,'userId':14,'projectId':3,'started':'2016-01-21T12:00:00+00:00','ended':'2016-01-21T19:30:00+00:00'},{'id':973,'userId':14,'projectId':3,'started':'2016-01-22T12:00:00+00:00','ended':'2016-01-22T13:58:00+00:00'},{'id':987,'userId':14,'projectId':3,'started':'2016-01-25T12:00:00+00:00','ended':'2016-01-25T20:00:00+00:00'},{'id':999,'userId':14,'projectId':3,'started':'2016-01-26T12:00:00+00:00','ended':'2016-01-26T18:00:00+00:00'},{'id':1030,'userId':14,'projectId':3,'started':'2016-01-27T12:00:00+00:00','ended':'2016-01-27T20:20:00+00:00'},{'id':1031,'userId':14,'projectId':3,'started':'2016-01-28T12:00:00+00:00','ended':'2016-01-28T18:11:00+00:00'},{'id':1045,'userId':14,'projectId':3,'started':'2016-01-29T12:17:36.828+00:00','ended':'2016-01-29T13:12:12.828+00:00'},{'id':1251,'userId':14,'projectId':3,'started':'2016-02-01T12:00:00+00:00','ended':'2016-02-01T17:30:00+00:00'},{'id':1252,'userId':14,'projectId':3,'started':'2016-02-02T12:00:00+00:00','ended':'2016-02-02T22:30:00+00:00'},{'id':1268,'userId':14,'projectId':3,'started':'2016-02-03T12:00:00+00:00','ended':'2016-02-03T19:45:00+00:00'},{'id':1368,'userId':14,'projectId':36,'started':'2016-02-03T19:45:45.513+00:00','ended':'2016-02-03T20:00:45.513+00:00'},{'id':1347,'userId':14,'projectId':3,'started':'2016-02-04T12:00:00+00:00','ended':'2016-02-04T18:00:00+00:00'},{'id':1367,'userId':14,'projectId':11,'started':'2016-02-04T18:00:45.513+00:00','ended':'2016-02-04T18:15:45.513+00:00'},{'id':1378,'userId':14,'projectId':3,'started':'2016-02-05T12:00:00+00:00','ended':'2016-02-05T21:15:00+00:00'},{'id':1380,'userId':14,'projectId':11,'started':'2016-02-05T21:15:00+00:00','ended':'2016-02-05T21:30:00+00:00'},{'id':1381,'userId':14,'projectId':36,'started':'2016-02-05T21:30:00+00:00','ended':'2016-02-05T21:45:00+00:00'},{'id':1379,'userId':14,'projectId':3,'started':'2016-02-06T12:00:00+00:00','ended':'2016-02-06T16:15:00+00:00'},{'id':1446,'userId':14,'projectId':3,'started':'2016-02-08T12:12:43.933+00:00','ended':'2016-02-08T17:12:43.933+00:00'},{'id':1447,'userId':14,'projectId':3,'started':'2016-02-09T12:00:00+00:00','ended':'2016-02-09T18:45:00+00:00'},{'id':1448,'userId':14,'projectId':3,'started':'2016-02-10T12:00:00+00:00','ended':'2016-02-10T18:00:00+00:00'},{'id':1449,'userId':14,'projectId':11,'started':'2016-02-10T12:00:00+00:00','ended':'2016-02-10T13:45:00+00:00'},{'id':1450,'userId':14,'projectId':3,'started':'2016-02-11T12:00:00+00:00','ended':'2016-02-11T18:15:00+00:00'},{'id':1451,'userId':14,'projectId':11,'started':'2016-02-11T12:00:00+00:00','ended':'2016-02-11T13:00:00+00:00'}]");
        }

        public WacsTimeKeepingEntry AddEntry(WacsTimeKeepingEntry newEntry) {
            int lastId = 0;
            if (entries.Count > 0) {
                lastId = entries.Max(e => e.Id);
            }
            newEntry.Id = lastId + 1;
            entries.Add(newEntry);
            return newEntry;
        }

        public WacsTimeKeepingProject AddProject(WacsTimeKeepingProject newProject) {
            throw new NotImplementedException();
        }

        public WacsTimeKeepingUser AddUser(WacsTimeKeepingUser newUser) {
            throw new NotImplementedException();
        }

        public bool DeleteEntry(int id) {
            var entry = entries.Where(e => e.Id == id).FirstOrDefault();
            if (entry == null) {
                return false;        
            }
            entries.Remove(entry);
            return true;
        }

        public bool DeleteProject(int id) {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int id) {
            throw new NotImplementedException();
        }

        public IQueryable<WacsTimeKeepingProject> Projects {
            get {
                return projects.AsQueryable();
            }
        }

        public IQueryable<WacsTimeKeepingUser> Users {
            get {
                return users.AsQueryable();
            }
        }

        public WacsTimeKeepingUser GetUserByName(string login) {
            return users.FirstOrDefault(u=>u.Login == @"MESTEKCORP\io_jwelty");
        }

        public WacsTimeKeepingUser GetAdminUserByName(string login) {
            return users.FirstOrDefault(u=>u.Login == @"MESTEKCORP\io_skerska");
        }

        public IQueryable<WacsTimeKeepingEntry> Entries {
            get {
                return entries.AsQueryable();
            }
        }

        public bool UpdateEntry(WacsTimeKeepingEntry updatedEntry) {
            var entry = entries.Where(e => e.Id == updatedEntry.Id).FirstOrDefault();
            if (entry == null) {
                return false;
            }
            entry.ProjectId = updatedEntry.ProjectId;
            entry.Started = updatedEntry.Started;
            entry.Ended = updatedEntry.Ended;
            return true;
        }

        public bool UpdateProject(WacsTimeKeepingProject updatedProject) {
            throw new NotImplementedException();
        }

        public bool UpdateUser(WacsTimeKeepingUser updatedUser) {
            throw new NotImplementedException();
        }
    }
}