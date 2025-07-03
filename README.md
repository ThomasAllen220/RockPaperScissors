Intent:
-Create a code submission for True Energy technical interview.
-Code submission should take approx 2 hours (I lost track of time having fun with this and spent around 3 hours working on this instead).
-Wanted to do this as an API utilizing TDD.

Issues:
-Currently I am having an update problem with Visual Studio, and the verion on my machine it is currently at stops the tests from building.
-No proof that tests return the desired results due to above issue, so this could be run on a different machine and not return expected results. If I were to go back, running a successful update would be the first step to find out whether or not the tests give the expected results.

Considerations:
-Projects (both API and TestClasses) build without problem.
-Projects both utilize .NET9.0 and most recent Nuget packages associates with testing.
-Designed as a bolt-on microservice, so ability to include within another app.

Reflection:
This was an ambitious attempt I took a little far, but, I had genuine fun with it! I'm more eager to find out if it would run properly, or whether I have missed anything and am disappointed that technical issues means that the tests don't run after the build. 
If this was to be run on a separate machine, the RockPaperScissorsTest.csproj needs to be it's own project scoped into the solution, with the GameTests.cs inside it. It will also require these nuget packages:
-Microsoft.AspNetCore.MVC.Testing 9.0.6
-Microsoft.NET.Test.Sdk 17.14.1
-xunit 2.9.3
-xunit.runner.visualstudio 3.1.1
