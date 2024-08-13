const fs = require('fs');

function listFiles(directory = './') {
  return new Promise((resolve, reject) => {
    if (!directory) {
      return reject(new Error('mkdir failed. Directory path is undefined'));
    }
    fs.readdir(directory, null, (e, files) => {
      if (e) {
        return reject(e);
      }
      if (Array.isArray(files)) {
        resolve(files);
      }
    })
  });

}

function makeDir(directory, options = null) {
  return new Promise((resolve, reject) => {
    console.log(`make dir ${directory}`);
    if (!directory) {
      return reject('mkdir failed. Directory path is undefined');
    }
    fs.mkdir(directory, options, (e, f) => {
      if (e) {
        return reject(e);
      }
      resolve(f || directory);
    });
  });
}

function removeDir(directory, options = null) {
  return new Promise((resolve, reject) => {
    console.log(`remove dir ${directory}`);
    if (!directory) {
      return reject('mkdir failed. Directory path is undefined');
    }
    fs.rm(directory, options || {
      recursive: true, force: true,
    }, (e, f) => {
      if (e) {
        return reject(e);
      }
      resolve(f || directory);
    });
  });
}

function copyFiles(source, destination, options = null) {
  return new Promise((resolve, reject) => {
    console.log(`copy files from ${source} to ${destination}`);
    if (!source) {
      return reject('copy files failed. Source path is undefined');
    }
    if (!destination) {
      return reject('copy files failed. Destination path is undefined');
    }
    fs.cp(source, destination, options || {recursive: true}, (e, f) => {
      if (e) {
        return reject(e);
      }
      resolve(f);
    });
  });
}

async function readFileString(path) {
  return new Promise((resolve, reject) => {
    console.log(`readFileString from ${path}`);
    if (!path) {
      return reject('readFileString failed. File path is undefined');
    }
    fs.readFile(path, 'utf8', (e, f) => {
      if (e) {
        return reject(e);
      }
      resolve(f);
    });
  });
}

async function writeFileString(path, data) {
  return new Promise((resolve, reject) => {
    console.log(`writeFileString to ${path}`);
    if (!path) {
      return reject('writeFileString failed. File path is undefined');
    }
    fs.writeFile(path, data, 'utf8', err => {
      if (err) {
        return reject(err);
      }
      resolve();
    });
  });
}

async function replaceStringInFile(path, searchValues, replaceValue) {
  console.log(`Replace ${searchValues.join(' or ')} with ${replaceValue} in file ${path}`);
  let data = await readFileString(path);
  if (data && typeof data === 'string') {
    for (const searchValue of searchValues) {
      data = data.replace(searchValue, replaceValue);
    }
    await writeFileString(path, data);
  } else {
    throw new Error('file is empty');
  }
}

module.exports = {copyFiles, makeDir, removeDir, listFiles, replaceStringInFile};
