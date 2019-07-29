const 
    webpack = require('webpack'),
    path = require('path'),
    HtmlWebpackPlugin = require("html-webpack-plugin"),
    CopyWebpackPlugin = require('copy-webpack-plugin'),
    MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = {
    entry: {
        app: ['./Resources/Styles/app.sass', './Resources/Scripts/app.ts']
    },
    output: {
        filename: '[name].js',
        path: path.resolve(__dirname, 'wwwroot'),
        publicPath: "/",
        library: "cqrsStarterTemplate",
        libraryTarget: "var"
    },
    resolve: {
        extensions: ['.ts', '.tsx']
    },
    module: {
        rules: [{
            test: /\.tsx?$/,
            use: [
                'ts-loader'
            ]
        }, {
            test: /\.woff2?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
            use: 'url-loader?limit=10000'
        }, {
            test: /\.(ttf|eot|svg)(\?[\s\S]+)?$/,
            use: 'file-loader'
        }, {
            test: /\.sass$/,
            use: [{
                loader: MiniCssExtractPlugin.loader,
                },
                'css-loader', 'sass-loader'
            ]}, {
                test: /\.(png|svg|jpg|gif)$/,
                use: [
                    'file-loader'
                ]
            }
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: '[name].css',
            chunkFilename: '[id].css',
            ignoreOrder: false
        }),
        new HtmlWebpackPlugin({
            inject: 'body',
            filename: '../Features/Shared/_Layout.cshtml',
            template: './Features/Shared/_Layout.Template.cshtml',
            hash: true,
            files: {
                css: ['styles.css'],
                js: ['app.js']
            },
            chucks: {
                head: {
                    css: ['styles.css']
                },
                main: {
                    js: ['app.js']
                }
            }
        }),
        new CopyWebpackPlugin([
             './Resources/Images/favicon.ico',
        ])
    ]
};