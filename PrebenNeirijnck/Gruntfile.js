/// <binding BeforeBuild='default' ProjectOpened='watch' />
module.exports = function (grunt) {
    'use strict';

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        bower_concat: {
            all: {
                dest: 'scripts/vendor.js',
                cssDest: 'css/vendor.css',
                mainFiles: {
                    'bootstrap': ['dist/css/bootstrap.css']
                },
                bowerOptions: {
                    relative: false
                }
            }
        },

        less: {
            development: {
                files: {
                    "css/core.css": "css/core.less"
                }
            }
        },

        concat: {
            scripts: {
                src: [],
                dest: 'scripts/core.js'
            }
        },

        uglify: {
            options: {
                compress: true
            },
            scripts: {
                files: {
                    'scripts/core.min.js': ['scripts/core.js'],
                    'scripts/vendor.min.js': ['scripts/vendor.js']
                }
            }
        },

        cssmin: {
            target: {
                files: [{
                    expand: true,
                    cwd: "css",
                    src: ['*.css', '!*.min.css'],
                    dest: 'css',
                    ext: '.min.css'
                }]
            }
        },

        watch: {
            less: {
                files: ['css/*.less', 'css/**/*.less'],
                tasks: ['less:development', 'cssmin'],
                options: {
                    livereload: true
                }
            },
            js: {
                files: ['scripts/components/*.js'],
                tasks: ['concat:scripts', 'uglify']
            }
        }
    });

    // The required plugins which will be loaded for task
    grunt.loadNpmTasks('grunt-bower-concat');
    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-watch');

    // The array of tasks
    grunt.registerTask('default', ['bower_concat', 'less', 'concat:scripts', 'uglify', 'cssmin']);
}