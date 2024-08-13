const {copyFiles, makeDir, replaceStringInFile} = require("../file-system-functions");
const {config} = require("./build-config");


async function main(config) {
  const projectName = config.projectName || null;
  const buildPath = config.buildPath || null;
  const outputPath = config.outputPath || 'publish';

  console.log('Start copy files with config', {projectName, buildPath, outputPath})
  await makeDir(`./${outputPath}`);
  await copyFiles(`./${buildPath}/.next/standalone/package.json`, `./${outputPath}/package.json`);
  await copyFiles(`./${buildPath}/.next/standalone/apps/${projectName}/`, `./${outputPath}/`);
  await replaceStringInFile(`./${outputPath}/server.js`, [
    `./..\\\\..\\\\${buildPath}\\\\.next`,
    `./../../${buildPath}/.next`
  ], './.next');
  // await replaceStringInFile(`./${outputPath}/test.js`, `./..\\\\..\\\\${buildPath}\\\\.next`, './.next');
  await copyFiles(`./${buildPath}/.next/standalone/node_modules/`, `./${outputPath}/node_modules/`);
  await copyFiles(`./${buildPath}/.next/standalone/${buildPath}/.next`, `./${outputPath}/.next/`);
  await copyFiles(`./${buildPath}/public`, `./${outputPath}/public`);
  await copyFiles(`./${buildPath}/.next/static`, `./${outputPath}/.next/static`);
  console.log('All files copied successfully');
}

main(config).then();
