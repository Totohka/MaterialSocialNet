const { printLogo } = require('../print-logo');
const { removeDir } = require('../file-system-functions');
const { config } = require('./build-config');

async function main() {
  printLogo();
  await removeDir(config.outputPath);
  await removeDir(config.buildPath);
}

main().then();
