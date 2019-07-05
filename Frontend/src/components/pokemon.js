
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