Visual Studio 2013 solution, open complile and run. Should see view as in screenshot provided.

Solution separated into:
	- ASP.NET application (Bundler)
	- Repository, contains persistance interface(Bundler.Repository)
	- Business logic, contains all product/bundle logic (Bundler.BusinessLogic)
	- Business logic unit tests used Moq and UnitTestFramework (Bundler.UnitTests)

Remarks
	- Task does not mention Pensioner account validation rules, added "must be 65+ rule"
