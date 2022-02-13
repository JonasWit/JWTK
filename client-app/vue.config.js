module.exports = {
    pwa: {
        name: 'systemywp',
        appleMobileWebAppCapable: 'yes',
        appleMobileWebAppStatusBarStyle: 'black',
        
        workboxPluginMode: 'InjectManifest',
        workboxOptions: {
            swSrc: 'dev/sw.js',
        },
        iconPaths: {
            msTileImage: 'img/icons8-gears-50.png'
        }
    }
}