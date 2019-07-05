(function(){function r(e,n,t){function o(i,f){if(!n[i]){if(!e[i]){var c="function"==typeof require&&require;if(!f&&c)return c(i,!0);if(u)return u(i,!0);var a=new Error("Cannot find module '"+i+"'");throw a.code="MODULE_NOT_FOUND",a}var p=n[i]={exports:{}};e[i][0].call(p.exports,function(r){var n=e[i][1][r];return o(n||r)},p,p.exports,r,e,n,t)}return n[i].exports}for(var u="function"==typeof require&&require,i=0;i<t.length;i++)o(t[i]);return o}return r})()({1:[function(require,module,exports){
let massEvolve = require('./massEvolve');

massEvolve();


},{"./massEvolve":2}],2:[function(require,module,exports){
module.exports = function () {

    if(getPokeSelect() === undefined) {
        return;
    }
    
    let pokemon = require('./pokemon.js');

    (function addPokemonToSelect() {
        pokemon.list.forEach(function (pokemon) {
            let pokemonSelect = getPokeSelect();

            if (pokemonSelect === undefined) {
                return;
            }

            let pokemonOption = document.createElement('option');
            pokemonOption.value = pokemon.name;
            pokemonOption.text = pokemon.name;

            pokemonSelect.add(pokemonOption, null);
        });
    })();

    (function pokemonFormOnSubmit() {
        let form = document.querySelectorAll('#pokemonForm')[0];
        if (form === null) {
            return null;
        }

        form.onsubmit = write;
    })();

    function write(e) {
        e.preventDefault();

        let pokemonSelect = getPokeSelect();
        let selectedPokemonName = pokemonSelect.options[pokemonSelect.selectedIndex].value;
        let pokemonAmounts = document.querySelectorAll('[name="pokemonAmount"]')[0].value;

        let numberOfTimesEvolve = calcNumEvolveTimes(selectedPokemonName, pokemonAmounts);


        let displayNumOfTimesEvolveBox = document.querySelectorAll('#pokemonEvolveResult')[0];
        displayNumOfTimesEvolveBox.value = `You can evolve ${selectedPokemonName}  ${numberOfTimesEvolve} times`;
    }

    function getPokeSelect() {
        return document.querySelectorAll('#pokemonSelect')[0];
    }

    function calcNumEvolveTimes(selectedPokemonName, pokemonAmounts) {
        let selectedPokemon = pokemon.get(selectedPokemonName);

        return Math.floor(pokemonAmounts / selectedPokemon.candy);
    }
};
},{"./pokemon.js":3}],3:[function(require,module,exports){

let pokemon = [
    pidgey = {
        "candy": 12,
        "name": "Pidgey",
        "types": [
            "normal", "flying"
        ]
    },
    caterpie =
    {
        "candy": 12,
        "name": "Caterpie",
        "types": [
            "bug"
        ]
    },
];

function  get (name) {
    return pokemon.find(function (pokemon) {
        return pokemon.name === name;
    });
}

exports.get = get;
exports.list = pokemon;
},{}]},{},[1]);
