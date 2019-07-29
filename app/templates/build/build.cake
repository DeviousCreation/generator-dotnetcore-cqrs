#addin "Cake.Npm"
#tool "nuget:?package=GitVersion.CommandLine"
#tool "nuget:?package=OctopusTools&version=4.15.5"
#tool "nuget:?package=xunit.runner.console&version=2.2.0"
#tool "nuget:?package=JetBrains.dotCover.CommandLineTools&version=2018.1.4"

var target = Argument<string>("target", "Default");
GitVersion gitversion;
var buildPath = Directory("./build-artifacts");
var publishPath = buildPath + Directory("publish");
var releasePath = buildPath + Directory("release");
var coverPath = buildPath + Directory("cover");


Task("__Clean")
    .Does(() => {
        if (BuildSystem.IsLocalBuild) {
            CleanDirectories(new DirectoryPath[] {
                buildPath
            });

            CleanDirectories("../**/bin");
            CleanDirectories("../**/obj");
        }  

        CreateDirectory(releasePath);
        CreateDirectory(publishPath);
        CreateDirectory(coverPath);   
    });

Task("__Versioning")
    .Does(() => {
        gitversion = GitVersion();        
        TeamCity.SetBuildNumber(gitversion.FullSemVer);
    });

Task("__RestorePackages")
    .Does(() => {
        var npmInstallSettings = new NpmInstallSettings();
        npmInstallSettings.FromPath("../Source/DeviousCreation.CqrsStarterTemplate.Web");
		npmInstallSettings.WithLogLevel(NpmLogLevel.Silent);
        NpmInstall(npmInstallSettings);
    });

Task("__Build")
    .Does(() => {
        var npmRunScriptSettings = new NpmRunScriptSettings{
           ScriptName = "release:build"
        };
		npmRunScriptSettings.WithLogLevel(NpmLogLevel.Silent);
        npmRunScriptSettings.FromPath("../Source/DeviousCreation.CqrsStarterTemplate.Web");
        NpmRunScript(npmRunScriptSettings);  

        var settings = new DotNetCoreBuildSettings {
            Configuration = "Release"
        };

        DotNetCoreBuild("../DeviousCreation.CqrsStarterTemplate.sln", settings);
    });
Task("__Test")
    .Does(() => {
        DotCoverCover(tool => {
            tool.DotNetCoreTest("../DeviousCreation.CqrsStarterTemplate.sln", new DotNetCoreTestSettings {
                Configuration = "Release" ,
        NoBuild = true,
        Logger = "console;verbosity=normal"   
            });
        },
        coverPath + File("result.dcvr"),
        new DotCoverCoverSettings()            
        );
        TeamCity.ImportDotCoverCoverage(MakeAbsolute(coverPath + File("result.dcvr")), "./");
    });
Task("__Publish")
    .Does(() => {
        var pubSettings = new DotNetCorePublishSettings {
            Configuration = "Release",
            OutputDirectory = publishPath
        };
        
        DotNetCorePublish("../source/DeviousCreation.CqrsStarterTemplate.Web/DeviousCreation.CqrsStarterTemplate.Web.csproj", pubSettings);

            });
Task("__Package")
    .Does(() => {
        Zip(publishPath, releasePath + File("DeviousCreation.CqrsStarterTemplate.Web.zip"));        
    });

Task("Build")
    .IsDependentOn("__Clean")
    .IsDependentOn("__Versioning")
    .IsDependentOn("__RestorePackages")
    .IsDependentOn("__Build")
    .IsDependentOn("__Publish")
    .IsDependentOn("__Package")
    ;

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);