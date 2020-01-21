// Initialize modules
// Importing specific gulp API functions lets us write them below as series() instead of gulp.series()
const { src, dest, watch, series, parallel } = require('gulp');
// Importing all the Gulp-related packages we want to use
const browserify = require("browserify");
const sourcemaps = require('gulp-sourcemaps');
const sass = require('gulp-sass');
const uglify = require('gulp-uglify');
const postcss = require('gulp-postcss');
const autoprefixer = require('autoprefixer');
const cssnano = require('cssnano');
var replace = require('gulp-replace');
const source = require("vinyl-source-stream");


// File paths
const files = { 
    scssPath: 'src/scss/*.scss',
    jsPath: 'src/**/*.js'
}

// Sass task: compiles the style.scss file into style.css
function scssTask(){    
    return src(files.scssPath)
        .pipe(sourcemaps.init()) // initialize sourcemaps first
        .pipe(sass()) // compile SCSS to CSS
        .pipe(postcss([ autoprefixer(), cssnano() ])) // PostCSS plugins
        .pipe(sourcemaps.write('.')) // write sourcemaps file in current directory
        .pipe(dest('../src/Rasolo.Web/assets/css')
    ); // put final CSS in dist folder
}

// JS task: concatenates and uglifies JS files to script.js
function jsTask(){
    return  (browserify({
        entries: ["./src/components/app.js"]
    })
               // Bundle it all up!
               .bundle()
               // Source the bundle
               .pipe(source("bundle.js"))
               // Then write the resulting files to a folder
               .pipe(dest("../src/Rasolo.Web/assets/js"))
    );
}


function cacheBustTask(){
    var cacheBustString = new Date().getTime();
    return src(['../src/Rasolo.Web/Views/Shared/_layout.cshtml'])
        .pipe(replace(/cb=\d+/g, 'cb=' + cacheBustString))
        .pipe(dest('../src/Rasolo.Web/Views/Shared/'));
}

// Watch task: watch SCSS and JS files for changes
// If any change, run scss and js tasks simultaneously
function watchTask(){
    watch([files.scssPath, files.jsPath], {interval: 1000, usePolling: true}, 
        series(
            parallel(scssTask, jsTask),
            cacheBustTask
        )
    );    
}

// Export the default Gulp task so it can be run
// Runs the scss and js tasks simultaneously
// then runs cacheBust, then watch task
exports.default = series(
    parallel(scssTask, jsTask), 
    cacheBustTask,
    watchTask
);