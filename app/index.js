'use strict';
const Generator = require('yeoman-generator');
const replace = require('gulp-string-replace');
const rename = require('gulp-rename');
module.exports = class extends Generator {

  initializing() {
    this.log("Installing CQRS template for .net Core...");
  }

  async prompting() {
    this.answers = await this.prompt([{
      type: 'input',
      name: 'name',
      message: 'Your project name',
      default: 'Some.Project'

    }]);
  }
  writing() {
    this.destinationRoot("./");
    const self = this;
    this.registerTransformStream([
      replace("DeviousCreation.CqrsStarterTemplate", this.answers.name),
      rename(function (path) {
        path.basename = path.basename.replace('DeviousCreation.CqrsStarterTemplate', self.answers.name)
        path.dirname = path.dirname.replace('DeviousCreation.CqrsStarterTemplate', self.answers.name)
      })
    ]);
    this.fs.copy(
      this.templatePath('./'),
      this.destinationPath(`./${this.answers.name}/`),
      { globOptions: { dot: true } }      
    );
  }

  install() {
    const name = this.answers.name;
    this.log("Restoring nuget packages...");
    this.spawnCommandSync("dotnet", [`restore`, `./${name}/`]);    
    this.log("Restoring npm packages...");
    this.spawnCommandSync('npm', ['install'], {cwd: `./${name}/source/${name}.Web`});
  }
};