(() => {
    function bell(ctx, destination, frequency, start, duration) {
        const tone = ctx.createOscillator();
        const overtone = ctx.createOscillator();
        const toneGain = ctx.createGain();
        const overtoneGain = ctx.createGain();

        tone.type = "sine";
        overtone.type = "sine";
        tone.frequency.setValueAtTime(frequency, start);
        overtone.frequency.setValueAtTime(frequency * 2.76, start);

        toneGain.gain.setValueAtTime(0.0001, start);
        toneGain.gain.exponentialRampToValueAtTime(0.2, start + 0.02);
        toneGain.gain.exponentialRampToValueAtTime(0.0001, start + duration);

        overtoneGain.gain.setValueAtTime(0.0001, start);
        overtoneGain.gain.exponentialRampToValueAtTime(0.045, start + 0.012);
        overtoneGain.gain.exponentialRampToValueAtTime(0.0001, start + duration * 0.45);

        tone.connect(toneGain).connect(destination);
        overtone.connect(overtoneGain).connect(destination);
        tone.start(start);
        overtone.start(start);
        tone.stop(start + duration + 0.05);
        overtone.stop(start + duration + 0.05);
    }

    function play() {
        const AudioContext = window.AudioContext || window.webkitAudioContext;
        if (!AudioContext) {
            return;
        }

        const ctx = new AudioContext();
        const master = ctx.createGain();
        master.gain.value = 0.85;
        master.connect(ctx.destination);

        const notes = [523.25, 659.25, 783.99, 1046.5, 783.99, 659.25, 880, 1046.5];
        notes.forEach((frequency, index) => {
            bell(ctx, master, frequency, ctx.currentTime + 0.04 + index * 0.23, 1.7);
        });

        window.setTimeout(() => ctx.close(), 5500);
    }

    document.addEventListener("click", (event) => {
        const opener = event.target.closest("[data-open-card]");
        if (!opener || opener.dataset.soundPlayed === "true") {
            return;
        }

        opener.dataset.soundPlayed = "true";
        play();
    }, true);
})();
