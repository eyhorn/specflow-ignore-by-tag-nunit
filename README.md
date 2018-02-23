# Ignore SpecFlow scenarios and features by tags for NUnit runner

## Usage
Tag your scenarios or features with tag like @Superseded, @Manual or @Draft (tags can be modified in `FeatureGeneration.targets`)

## Examples
Scenario level tag
```gherkin 
Feature: Calculations
    In order to perform basic calculations
    As a user of this program
    I want to be told results of calculations
    
@Superseded
Scenario: Perform calculations
    Given An outdated test
    When I perform outdated operation
    Then Some Result will be produced    
```
Feature level tag
```gherkin
@Draft
Feature: NewFunctions
	

Scenario: Draft scenario
	Given I have a number 90	
	When I take sinus of the number
	Then I will receive 1
```

## Implementation
Add `FeatureGeneration.targets` file to your project
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="4.0" DefaultTarget="GeneratePatchedFeatureFiles" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="..\packages\SpecFlow.2.3.0\tools\TechTalk.SpecFlow.targets" Condition="Exists('..\packages\SpecFlow.2.3.0\tools\TechTalk.SpecFlow.targets')" />
     
  <Target Name="GeneratePatchedFeatureFiles" DependsOnTargets="UpdateFeatureFilesInProject">
    <UpdateFeatureFile FeatureFiles="@(SpecFlowFeatureFiles->'%(RelativeDir)\%(Filename).feature.cs')" SkippedTags ="Superseded|Manual|Draft"/>
  </Target>
  
  <UsingTask TaskName="UpdateFeatureFile" TaskFactory="CodeTaskFactory" AssemblyFile="$(MSBuildToolsPath)\Microsoft.Build.Tasks.v4.0.dll">
    <ParameterGroup>
      <FeatureFiles ParameterType="Microsoft.Build.Framework.ITaskItem[]" Required="true" />
      <SkippedTags ParameterType="System.String" Required="true" />
    </ParameterGroup>
    <Task>
      <!--
      Go through generated feature.cs files and patch if not already patched
      Patching will skip all scenarios marked with predefined tags
      -->
      <Reference Include="System.Core" />
      <Using Namespace="System" />
      <Using Namespace="System.IO" />
      <Using Namespace="System.Text.RegularExpressions" />
      <Code Type="Fragment" Language="cs">
        <![CDATA[      
        foreach(var featureFile in FeatureFiles) {            
            var automaticallyPatchedFlag = "//Automatically patched";
            var input = File.ReadAllText(featureFile.ToString());
            if(input.EndsWith(automaticallyPatchedFlag)) continue;
            
            Log.LogMessage("Patching feature file " + featureFile.ToString());
            input = Regex.Replace(input, @"\[NUnit.Framework.CategoryAttribute\(""("+ SkippedTags + @")""\)\]",
              "$&\r\n[NUnit.Framework.IgnoreAttribute(\"Test not run as it was tagged: $1\")]", RegexOptions.IgnoreCase);
            input += automaticallyPatchedFlag;
            File.WriteAllText(featureFile.ToString(), input);
        }
          ]]>
      </Code>
    </Task>
  </UsingTask>
</Project>
```

Then add following lines to your .csproj file 
```xml
  <Import Project="FeatureGeneration.targets" />
  <Target Name="BeforeBuild" DependsOnTargets="GeneratePatchedFeatureFiles" />
```