=========================================================================================================================
: : : : : : : : : : Royal London : : : : : : : : : :
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Insurance Renewal Reminder
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Creating Invitation letters & reminders for Insurance renewal for customers of Royal London Insurance
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
This project is for creating Reminder text files for each individual customer depend upon inout file given.
=========================================================================================================================
INTRODUCTION ::

This is web portal for Royal London Insurance Renewal department empoyee for generating text file of reminders (mail/email) to be sent to clients for their Insurance renewal. One has to select Input (CSV) file with data of clients and this utility will automatically create the text files.

=========================================================================================================================
REQUIREMENT ::

The application should create Renewal invitation letters to customers from a ficticious insurance company. It should generate a Text file containing the body of the letter for each customer record in an input file. The name of each generated text file should include customers ID and Name, and the application should not generete a text file if there is already one of the same customer in the output location.

=========================================================================================================================
INSTRUCTION (HOW TO USE) ::

1. Install the project on local or server and Run
2. On page there is Navigation/Header, file upload control and Button to process.
3. Please select CSV file as input for generating mail reminders (TestFiles/*.CSV)
4. Click on "Create Reminder" to create the reminder templates.
5. Check the response message and act accordingly.
6. Reminders are created in "Output" folder of root folder of project.
7. Note, One can change the path of Input/Output folder as well as Template using "Web.Config"
8. Also, Template verbage can be change which is kept at "Input" folder. However any change in "#{InputKey}#" may require subsequent change in project as         well.

=========================================================================================================================
PROJECT STRUCTURE ::

This application is build on Microsoft .NET technology with Web application. N-Tier application concept is been used here and seperate projects are added for separate fuctionality. Solution consict of following projects
1. InsuranceRenewalTypes
2. InsuranceRenewalCalculator
3. EventLogHandler
4. InsuranceRenewalReminderDemo
5. InsuranceRenewalReminderDemo.Tests

1. InsuranceRenewalTypes :
This project consist of complex types that are used in project. A "ResponseBase" is create to have base class for all derived class that will be return as output parameter for any service/Processing call. "InputField" is a Type that is created to match the Input file values, Any change in Input CSV file structure in future needs updation in class as well. Inheritance is used here for creating objects however if project goes further in larger requirement that Co-Varience concept and Factory design pattern can be seen while creating object of more complex objects.

2. InsuranceRenewalCalculator :
This project is focus on acting as caclulator for calclulating individual`s Insurance Renewal amounts. There are many terms to be calculated on basic information provided of the client in input file. Business leyer logic can be placed here. Below are some of the calculation that needs to be made.

  a) CreditCharge :  		An additional charge that applies when paying monthly by Direct Debit which is 5% of the annual premium, rounded up to a 					whole number of pound and pence

  b) TotalPremium : 		The total amount payable for the year when paying monthly by Direct Debit which is annual premuim plus the credit charge.

  c) AvgMonthlyPremium : 	The average amount payable per month when paying monthly by Direct Debit which is Total Premium (Direct Debit) divided by 					the number of the months in a year.

  d) InitialMonthlyPayment :	The amount of the First monthly payments which will equal the Average Monthly Premium if that is a whole number of pound and 				pence; otherwise will be higher.

  e) OtherMonthlyPayment :       The amount of the other 11 monthly payments which will equal the Average Monthly Premium if that is a whole number of pound				and pence; otherwise will be lower.

3. EventLogHandler : 
This project is many utility added for error/exception handelling and overall project enhancement. This utility writes exception,error, warning, information event logs in log file. Consuming application should specify path and folder for such logs to be saving. Path for the logs fie must be specify in AppSetting of consuming application, In case such AppSetting Key is not present in consuming application`s WebSetting file then utility will create file pplication`s root file. File name is kept as date, Hence everyday new file will be genereted and kept in ErrorLogs folder.
As the project enhances, Reflection can be added into this project so as to get  calling DLL information.

4. InsuranceRenewalReminderDemo : 
This is the main project having UI to navitate the user to perform the task. Main componants of this project are as follows

 a) Default.ASPX : 	This is main page(ASPX) of the website. In this page UI design and its backgroun coding is implemented for user to upload the correct 				CSV file and to get output reminder text files.

 b) Web.Config :	This file contents configarations for the project. AppSetting is main that to be discussed as it contens value of multiple key variables used 			in the project such as Path of Input/Output and Template file as well as number of input to be consider at once.

 c) Utility :	 	This folder is utility folder for UI project. This contens mainly two files/classes as one with helper classes and other with reading template 				class. Helper class is created to have static helper classes to read the input CSV file uploaded by user as well as creating output files 				using same inut data. It also has methods to fill data in template file. Please note there can be two approach of how to fill the data we have 			used the approach in which we have placed #token# fields in template and replacing that token one by one using its key. This is lenghy 				coding method however its readable and scalable. There is another approach to this is to have {0} simple inline filling in template and 				replace that using ordered value given in string.Format, however readability and scalability is compromised in this way. The another class 				that we have over here is Reading Template file, Since all the user will be having same template as of now hence we are following 				Singleton pattern here with sealed class and lock mechanism.

 d) Scripts :	This folder has all Javasript and CSS files needed for validation and styles.

 e) ErrorLogs : 	This folder contens event log file on daily basis. Please note that mapping of this folder is done at Web.Config file. To change output to 				different folder, kinldy change mapping from Web.Config AppSetting entry.

 f) Input : 		This is folder created for keeping Templates for the reminder as well as to store Input file uploaded by user for temporary purpos. Please 			note that mapping of this folder is done at Web.Config file. To change Input path to different folder, kinldy change mapping from 				Web.Config AppSetting entry.

 g) Output : 	This folder will store output genereted from the project after uploading correct Input files. Please note that mapping of this folder is done at 		Web.Config file. To change output to different folder, kinldy change mapping from Web.Config AppSetting entry.

 h) TestFiles : 	This folder is created for testing purpose and may or maynot be part of entire solution. This folder contents test CSV and Template files of 		the project for testing purpose.

5. InsuranceRenewalReminderDemo.Tests :
Test project is created to perform unit text on project. Nunit testing feature is applied here. Tests mainly focus on testing business layer logic or calculations/
On the UI part is also tested.

=========================================================================================================================
NOTE :: Submitted by : Mayur Gharat, India, (+91) 9773572804, mayur.gharat@tcs.com
=========================================================================================================================
