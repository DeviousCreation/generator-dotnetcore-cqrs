const 
    { CleanWebpackPlugin } = require('clean-webpack-plugin'),
    merge = require('webpack-merge'),
    common = require('./webpack.common.js');

module.exports = merge(common, {
    devtool: 'inline-source-map',
    plugins: [
        new CleanWebpackPlugin()        
    ]
});