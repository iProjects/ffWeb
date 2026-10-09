document.addEventListener("DOMContentLoaded", function () {
	var spriteContainer = document.querySelector(".svg-sprite-definitions");
	if (!spriteContainer) {
		console.warn("SVG Sprite partial view (_SvgSprite) is not rendered in the current view.");
	}
});