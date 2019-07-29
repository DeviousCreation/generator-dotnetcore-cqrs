const
    merge = require('webpack-merge'),
    common = require('./webpack.common.js'),
    OptimizeCssAssetsPlugin = require('optimize-css-assets-webpack-plugin'),
    UglifyJsPlugin = require('uglifyjs-webpack-plugin');


module.exports = merge(common, {
    plugins: [
        new OptimizeCssAssetsPlugin({
            cssProcessorOptions: { discardComments: { removeAll: true } }
        }),
        new UglifyJsPlugin({
            uglifyOptions: {
                output: {
                    comments: false
                }
            }
        })
    ]
});