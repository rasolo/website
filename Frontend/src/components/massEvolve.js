module.exports = function () {

    if (getPokeSelect() === undefined) {
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