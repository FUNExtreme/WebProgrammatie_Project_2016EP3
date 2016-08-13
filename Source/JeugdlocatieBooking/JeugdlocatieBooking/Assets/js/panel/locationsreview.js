window.onload = function()
{
    var facilityRatingGroups = document.getElementsByClassName('facility-rating');
    var facilityRatingGroupsCurrentlySelectedElement = Array(facilityRatingGroups.length);
    for (var x = 0; x < facilityRatingGroups.length; x++)
    {
        handleRadioGroup(x);
    }

    function handleRadioGroup(index)
    {
        var facilityRatingInputs = facilityRatingGroups[index].getElementsByClassName('rating-input');
        for (var y = 0; y < facilityRatingInputs.length; y++)
        {
            // Check if it is currently already checked
            if (facilityRatingInputs[y].checked)
            {
                facilityRatingGroupsCurrentlySelectedElement[index] = facilityRatingInputs[y];
                facilityRatingInputs[y].parentElement.classList.add('checked');
            }

            facilityRatingInputs[y].addEventListener('change', function ()
            {
                if (facilityRatingGroupsCurrentlySelectedElement[index] != undefined)
                    facilityRatingGroupsCurrentlySelectedElement[index].parentElement.classList.remove('checked');

                if (this.checked)
                {
                    facilityRatingGroupsCurrentlySelectedElement[index] = this;
                    this.parentElement.classList.add('checked');
                }
            });
        }
    }
}