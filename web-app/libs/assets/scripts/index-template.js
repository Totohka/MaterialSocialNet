const path = require('path');

function upperFirstLetter(str) {
  if (!str) {
    return str;
  }
  if (typeof str !== 'string') {
    return str;
  }
  if (str.length === 0) {
    return str;
  }
  const f = str[0].toUpperCase();
  const other = str.slice(1, str.length).toLowerCase();
  return f + other;
}

function kebabToCamel(str) {
  if (!str) {
    console.log('!str');
    return str;
  }
  if (typeof str !== 'string') {
    console.log('type is ', typeof str);
    return str;
  }
  if (str.length === 0) {
    return str;
  }
  return str.split('-').map(upperFirstLetter).join('');
}

function defaultIndexTemplate(filePaths) {
  const exportEntries = filePaths.map(({ path: filePath, originalPath }) => {
    const basename = path.basename(filePath, path.extname(filePath));
    const exportName = `${kebabToCamel(basename)}Icon`;
    return `export { default as ${exportName} } from './${basename}'`;
  });
  return exportEntries.join('\n');
}

module.exports = defaultIndexTemplate;
