module.exports = {
  '*.{ts,tsx}': (files) => {
    const baseDir = 'QVAWeb/qvaweb';
    let workFolder = `${process.cwd()}`;
    if (/^win/i.test(process.platform)) {
      workFolder = workFolder.replace(/\\/g, '/');
    }
    const relativeFiles = files.map((f) => f.replace(`${workFolder}/`, ''));
    const fileList = relativeFiles.join(' ');
    const fileListRelativeToGitDir = relativeFiles.map((f) => `${baseDir}/${f}`).join(' ');
    return [
      `./node_modules/.bin/eslint ${fileList}`,
      `prettier --write ${fileList}`,
      `git add ${fileListRelativeToGitDir}`,
    ];
  },
};