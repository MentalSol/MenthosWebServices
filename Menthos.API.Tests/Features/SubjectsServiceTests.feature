Feature: SubjectsServiceTests
As a Developer
I want to add new Subject through API
In order to make it available for applications.

	Background:
		Given the Endpoint https://localhost:7125/api/v1/subjects is available

	@subject-adding
	Scenario: Add Subject with unique Name
		When a Post Request is sent
		  | Name      | Image        |
		  | Calculo I | Calculo1.png |
		Then A Response is received with Status 200
		And a Subject Resource is included in Response Body
		  | Id | Name      | Image        |
		  | 1  | Calculo I | Calculo1.png |
    
	@subject-adding
	Scenario: Add Subject 2 with unique Name
		When a Post Request is sent
		  | Name       | Image        |
		  | Calculo II | Calculo2.png |
		Then A Response is received with Status 200
		And a Subject Resource is included in Response Body
		  | Id | Name       | Image        |
		  | 1  | Calculo II | Calculo2.png |
    
	@subject-adding
	Scenario: Add Subject 3 with unique Name
		When a Post Request is sent
		  | Name              | Image                |
		  | Matematica Basica | MatematicaBasica.png |
		Then A Response is received with Status 200
		And a Subject Resource is included in Response Body
		  | Id | Name              | Image                |
		  | 1  | Matematica Basica | MatematicaBasica.png |
    
	@subject-adding
	Scenario: Add Subject 4 with unique Name
		When a Post Request is sent
		  | Name                     | Image                       |
		  | Matematica Computacional | matematicaComputacional.png |
		Then A Response is received with Status 200
		And a Subject Resource is included in Response Body
		  | Id | Name                     | Image                       |
		  | 1  | Matematica Computacional | matematicaComputacional.png |
    
	@subject-adding
	Scenario: Add Subject 5 with unique Name
		When a Post Request is sent
		  | Name           | Image             |
		  | Programacion I | Programacion1.png |
		Then A Response is received with Status 200
		And a Subject Resource is included in Response Body
		  | Id | Name           | Image             |
		  | 1  | Programacion I | Programacion1.png |
    
	@subject-adding
	Scenario: Add Subject 6 with unique Name
		When a Post Request is sent
		  | Name            | Image             |
		  | Programacion II | Programacion2.png |
		Then A Response is received with Status 200
		And a Subject Resource is included in Response Body
		  | Id | Name            | Image             |
		  | 1  | Programacion II | Programacion2.png |
    
	@subject-adding
	Scenario: Add Subject 7 with unique Name
		When a Post Request is sent
		  | Name     | Image       |
		  | Fisica I | Fisica1.png |
		Then A Response is received with Status 200
		And a Subject Resource is included in Response Body
		  | Id | Name     | Image       |
		  | 1  | Fisica I | Fisica1.png |
    
	@subject-adding
	Scenario: Add Subject 8 with unique Name
		When a Post Request is sent
		  | Name      | Image       |
		  | Fisica II | Fisica2.png |
		Then A Response is received with Status 200
		And a Subject Resource is included in Response Body
		  | Id | Name      | Image       |
		  | 1  | Fisica II | Fisica2.png |
    
	@subject-adding
	Scenario: Add Subject 9 with unique Name
		When a Post Request is sent
		  | Name    | Image       |
		  | Quimica | Quimica.png |
		Then A Response is received with Status 200
		And a Subject Resource is included in Response Body
		  | Id | Name    | Image       |
		  | 1  | Quimica | Quimica.png |