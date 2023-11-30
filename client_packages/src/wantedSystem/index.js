global.localplayer = mp.players.local;

mp.events.add("client.wanted.viewStars", (valueStars) => 
{
    mp.console.logInfo("stars");
    mp.game.invoke("0x1454F2448DE30163", valueStars);
});

mp.events.add("render", () => {
    if (!localplayer.hasVariable("IS_COP") || !localplayer.getVariable("IS_COP"))
        return;

    mp.players.forEachInStreamRange(player => {
        if (!player || player == localplayer)
            return;

        if (!player.hasVariable("WANTED_STARS") || player.getVariable("WANTED_STARS") < 3)
        {
            mp.console.logInfo("Player hasn't wanted or has a 2 and low stars");
            return;
        }
        
        const wantedLvl = player.getVariable("WANTED_STARS");
        
        if (wantedLvl == 5) {
            mp.blips.new(1, player.position, {
                name: "Wanted player"
            });

            return;
        }
        
        const playerPositions = localplayer.position;
        const targetPos = player.position;

        const distance = mp.game.gameplay.getDistanceBetweenCoords(playerPositions.x, playerPositions.y, playerPositions.z, targetPos.x, targetPos.y, targetPos.z, true);

        if (distance <= 100 && wantedLvl == 4)
            mp.game.graphics.notify("~b~ Wanted player in 100 meters");

        else if (distance <= 20 && wantedLvl == 3)
            mp.game.graphics.notify("~b~ Wanted player in 20 meters");
    });
});