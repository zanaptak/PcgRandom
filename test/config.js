module.exports = {
  entry: "PcgRandom.Tests.fsproj",
  outDir: "bld",
  babel: {
    plugins: ["@babel/plugin-transform-modules-commonjs"],
    sourceMaps: false,
  },
  fable: {
    define: [ "ZANAPTAK_NODEJS_CRYPTO" ]
  }
}
