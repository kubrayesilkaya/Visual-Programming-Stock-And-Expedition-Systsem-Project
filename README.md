# Visual-Programming-Stock-And-Expedition-Systsem-Project
Visual Programming Stock And Expedition Systsem Project

#### This project is a Windows Form App. It has been built using the .NET Framework.

#### This project is an enhanced version of the project I previously worked on, which can be found at the following link: 
https://github.com/kubrayesilkaya/Visual-Programming-Project. 
To get information about the parts other than the newly added features, you can take a look at the link provided.

#### USE-CASE DIAGRAM :

![Use case diagram](https://github.com/kubrayesilkaya/Visual-Programming-Stock-And-Expedition-Systsem-Project/assets/93487264/22f18db4-263e-4e55-8f1c-03857e2a329b)

#### ENTITY_RELATIONSHIP DIAGRAM (ERD) :

![Database ER diagram (crow's foot)](https://github.com/kubrayesilkaya/Visual-Programming-Stock-And-Expedition-Systsem-Project/assets/93487264/f6142fd7-5199-498a-9cbd-fca950fc4d9b)


There is a system that provides real-time tracking between producer-consumer factories and warehouses. 

Consumer factories make product requests from the warehouses they are connected to, and if the warehouse approves the request, it enters the necessary date information. When the request is recorded in the system as 'delivered' by the warehouse, a verification message is prompted to confirm whether the products have successfully reached the factory. 

All data is recorded in the database with all the details. Attention has been paid to preventing redundancy in the database design.

A filtering feature has been implemented in the report page to allow requests to be filtered by dates.

When a consumer enters a request and presses the button at the factory, a unique request number is assigned to that request and shown to the user.

![consumer](https://github.com/kubrayesilkaya/Visual-Programming-Stock-And-Expedition-Systsem-Project/assets/93487264/1482a174-0c28-48eb-bcca-d4b92218e654)



The factory can obtain information about the request by entering the request number.

![exp factory not approved](https://github.com/kubrayesilkaya/Visual-Programming-Stock-And-Expedition-Systsem-Project/assets/93487264/79be9f39-fb18-4161-a9f8-3b33373ebe91)

If the status of the request is recorded as 'delivered,' a confirmation message is displayed to the factory, and the response is saved.

![exp factory](https://github.com/kubrayesilkaya/Visual-Programming-Stock-And-Expedition-Systsem-Project/assets/93487264/159c2524-9271-4bed-86b4-970e6beba23f)


The warehouse can update the necessary information, but the static information is retrieved from the database; estimated labeled dates are also systematic.

![wh exp](https://github.com/kubrayesilkaya/Visual-Programming-Stock-And-Expedition-Systsem-Project/assets/93487264/27e5f1b4-00ce-4da3-8c71-7858757570af)

![raport](https://github.com/kubrayesilkaya/Visual-Programming-Stock-And-Expedition-Systsem-Project/assets/93487264/24566e47-bfb5-4bc1-976f-cedd5b6c1c46)

