// Initialize modules
// Importing specific gulp API functions lets us write them below as series() instead of gulp.series()
const { src, dest, watch, series, parallel } = require('gulp');
// Importing all the Gulp-related packages we want to use
const sass = require('gulp-sass')(require('sass'));
const concat = require('gulp-concat');
const terser = require('gulp-terser');
const postcss = require('gulp-postcss');
const autoprefixer = require('autoprefixer');
const cssnano = require('cssnano');
const replace = require('gulp-replace');
const browsersync = require('browser-sync').create();
const browserify = require('browserify');
var source = require('vinyl-source-stream');
// File paths
const files = {
	scssPath: 'src/scss/*.scss',
    jsPath: 'src/components/*.js'
};

// Sass task: compiles the style.scss file into style.css
function scssTask() {
	return src(files.scssPath, { sourcemaps: true }) // set source and turn on sourcemaps
		.pipe(sass()) // compile SCSS to CSS
		.pipe(postcss([autoprefixer(), cssnano()])) // PostCSS plugins
		.pipe(dest('../src/Rasolo.Web/wwwroot/assets/css'))
}

// JS task: concatenates and uglifies JS files to script.js
function jsTask() {
	return browserify({entries: ['src/components/app.js']})
    .bundle()
    //Pass desired output filename to vinyl-source-stream
    .pipe(source("bundle.js"))
    // Start piping stream to tasks!
    .pipe(dest('../src/Rasolo.Web/wwwroot/assets/js'));
}


// Browsersync to spin up a local server
function browserSyncServe(cb) {
	// initializes browsersync server
	browsersync.init({
		server: {
			baseDir: '.',
		},
		notify: {
			styles: {
				top: 'auto',
				bottom: '0',
			},
		},
	});
	cb();
}
function browserSyncReload(cb) {
	// reloads browsersync server
	browsersync.reload();
	cb();
}

function cacheBustTask(){
	var cacheBustString = new Date().getTime();
    return src(['../src/Rasolo.Web/Views/Shared/_Layout.cshtml'])
        .pipe(replace(/\\?cb=[^"]*/g, 'cb=' + cacheBustString))
        .pipe(dest('../src/Rasolo.Web/Views/Shared/'));
}

// Watch task: watch SCSS and JS files for changes
// If any change, run scss and js tasks simultaneously
function watchTask() {
	watch(
		[files.scssPath, files.jsPath],
		{ interval: 1000, usePolling: true }, //Makes docker work
		series(parallel(scssTask, jsTask), cacheBustTask)
	);
}

// Browsersync Watch task
// Watch HTML file for change and reload browsersync server
// watch SCSS and JS files for changes, run scss and js tasks simultaneously and update browsersync
function bsWatchTask() {
	watch('index.html', browserSyncReload);
	watch(
		[files.scssPath, files.jsPath],
		{ interval: 1000, usePolling: true }, //Makes docker work
		series(parallel(scssTask, jsTask), browserSyncReload)
	);
}

// Export the default Gulp task so it can be run
// Runs the scss and js tasks simultaneously

exports.dist = series(parallel(scssTask, jsTask), cacheBustTask);

exports.watch = series(parallel(scssTask, jsTask), watchTask);


// Runs all of the above but also spins up a local Browsersync server
// Run by typing in "gulp bs" on the command line
exports.bs = series(
	parallel(scssTask, jsTask),
	browserSyncServe,
	bsWatchTask
);