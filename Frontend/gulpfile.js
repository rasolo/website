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
const gulp = require('gulp');


// File paths
const files = { 
    scssPath: 'src/scss/*.scss',
    jsPath: 'src/**/*.js'
}


async function scssTask(){   
 gulp.task('scssTask', async function() {
    return src(files.scssPath)
    .pipe(sourcemaps.init()) // initialize sourcemaps first
    .pipe(sass()) // compile SCSS to CSS
    .pipe(postcss([ autoprefixer(), cssnano() ])) // PostCSS plugins
    .pipe(sourcemaps.write('.')) // write sourcemaps file in current directory
    .pipe(dest('../src/Rasolo.Web/wwwroot/assets/css'))
  });
}

async function jsTask() {
gulp.task('jsTask', async function() {
    return  (browserify({
        entries: ["./src/components/app.js"]
    })
               // Bundle it all up!
               .bundle()
               // Source the bundle
               .pipe(source("bundle.js"))
               // Then write the resulting files to a folder
               .pipe(dest("../src/Rasolo.Web/wwwroot/assets/js"))
    );
  });
}
// Watch task: watch SCSS and JS files for changes
// If any change, run scss and js tasks simultaneously
function watchTask(){
    watch([files.scssPath, files.jsPath], {interval: 1000, usePolling: true}, 
        series(
            parallel(scssTask, jsTask),
        )
    );    
}

// Export the default Gulp task so it can be run
// Runs the scss and js tasks simultaneously
// then runs cacheBust, then watch task
exports.default = series(
    parallel(scssTask, jsTask), 
    watchTask
);

exports.dist = series(
    parallel(scssTask, jsTask), 
);