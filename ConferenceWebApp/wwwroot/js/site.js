// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function onConferenceClick(element) {
    let parent = element.parentNode.parentNode
    let additional_info_block = parent.getElementsByClassName("conf_addition_info")[0]
    if (additional_info_block != null) {
        isDisabled = additional_info_block.style.display === 'none'
        additional_info_block.style.display = isDisabled ? 'flex' : 'none';
    }
}