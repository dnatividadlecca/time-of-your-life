import { useState, useEffect } from 'react'
import ClockProps from './ClockProps'

function SetClockProps(props) {
    const showPanel = props.showPanel;
    const togglePanel = props.togglePanel;

    const clockProps = new ClockProps()
    const [fontFamily, setFontFamily] = useState(clockProps.fontFamily)
    const [blinkColons, setBlinkColons] = useState(clockProps.blinkColons)
    const [presets, setPresets] = useState([])
    const [loading, setLoading] = useState(true)

    const [TempfontFamily, setTempFontFamily] = useState(fontFamily)
    const [titlefontSize, setTitleFontSize] = useState(clockProps.titleFontSize);
    const [clockfontSize, setClockFontSize] = useState(clockProps.clockFontSize);

    const [titleColor, setTitleColor] = useState(clockProps.titlefontColor);
    const [clockColor, setClockColor] = useState(clockProps.clockfontColor);

    const [selectedPresetId, setSelectedPresetId] = useState(null);

    const [titleColors, setTitleColors] = useState([clockProps.titlefontColor]);
    const [clockColors, setClockColors] = useState([clockProps.clockfontColor]);

    // Function to update a specific color
    const handleTitleColorChange = (index, newColor) => {
        const updatedColors = [...titleColors];
        updatedColors[index] = newColor;
        setTitleColors(updatedColors);
        setClockProps();
    };

    const handlClockColorChange = (index, newColor) => {
        const updatedColors = [...clockColors];
        updatedColors[index] = newColor;
        setClockColors(updatedColors);
        setClockProps();
    };

    useEffect(() => {
        fetchPresets();
    }, []);

    const fetchPresets = async () => {
        try {
            const response = await fetch('clock/presets');
            if (response.ok) {
                const data = await response.json();
                if (data.length === 0) {
                    alert('There are no presets saved. Default values will load')
                }

                setPresets(data);
                setLoading(false)
            } else {
                console.error('Failed to load presets');
            }
        } catch (error) {
            console.error('Error fetching presets:', error);
        }
    };

    const getProps = () => {
        const props = new ClockProps()
        props.fontFamily = document.getElementById('fontFamily').value
        props.titleFontSize = document.getElementById('titleFontSizeSlide').value
        props.clockFontSize = document.getElementById('clockFontSizeSlide').value
        //props.titlefontColor = document.getElementById('titleFontColor').value
        props.titlefontColor = document.getElementById('titleFontColorPicker').value
        props.clockfontColor = document.getElementById('clockfontColor').value
        props.blinkColons = document.getElementById('blinkColons').checked
        return props
    }

    const setClockProps = () => {
        const setProps = getProps()
        props.setClockProps(setProps)
    }

    const setClockPropsFromCheckBox = (preset) => {
        if (preset !== null) {
            //alert(preset.titleFontColor)
            document.getElementById('fontFamily').value = preset.fontFamily;
            setTempFontFamily(preset.fontFamily)

            document.getElementById('titleFontSizeSlide').value = preset.titleFontSize;
            setTitleFontSize(preset.titleFontSize)

            document.getElementById('titleFontColorPicker').value = preset.titleFontColor;
            document.getElementById('titleFontColor').value = preset.titleFontColor;
            ////setTitleColor(preset.titleFontColor)
            //setTitleColor(preset.titleFontColor)

            document.getElementById('clockFontSizeSlide').value = preset.clockFontSize;
            setClockFontSize(preset.clockFontSize)

            document.getElementById('clockFontColorPicker').value = preset.clockFontColor;
            document.getElementById('clockfontColor').value = preset.clockFontColor;
            //setClockColor(preset.clockFontColor)
        }
        setClockProps();
        setTitleUI();
        setClockUI();
    };

    const setTitleUI = () => {
        setTitleColors([document.getElementById('titleFontColorPicker').value])
        clockProps.titleColors = document.getElementById('titleFontColorPicker').value
    }

    const setClockUI = (e) => {
        setClockColors(([document.getElementById('clockFontColorPicker').value]) )
        clockProps.clockColors = document.getElementById('clockFontColorPicker').value
    }

    const setBlinkColonsUI = () => {
        setBlinkColons(document.getElementById('blinkColons').checked)
        clockProps.blinkColons = document.getElementById('blinkColons').checked
        setClockProps()
    }

    const presetsDisplay = (() => {
        console.log(presets)
        return loading ? (
            <div>
                This is a good place to display and use the presets stored on the sever.
            </div>
            ) : (
            <ul>
                {presets.map((p, i) => (
                <div key={p.id}>
                    <input
                        type="checkbox"
                        checked={selectedPresetId === p.id}
                        onChange={() => handlePresetSelection(p.id)}
                    />
                    <span className="checkbox-label">
                        Preset {p.id}:{' '}
                        {`Font: ${p.fontFamily}, Title Color: ${p.titleFontColor}, Clock Color: ${p.clockFontColor}, Title Size: ${p.titleFontSize}, Clock Size: ${p.clockFontSize}`}
                    </span>
                </div>
                ))}
            </ul>
            )
    })()

    const handleFontTitleColorChange = (event) => {
        setTitleColor(event.target.value);
    };

    const handleFontTitleColorKeyDown = (event) => {
        if (event.key === 'Enter' || event.key === 'Tab') {
            if (titleColor.length < 1) {
                alert("Font Color can't be empty.");
            } else {
                setTempFontFamily(titleColor.value)
                setClockProps();
            }
        }
    };

    const handleFontTitleColorBlur = () => {
        if (titleColor.length < 1) {
            alert("Font Color can't be empty.");
        } else {
            setTempFontFamily(titleColor.value)
            setClockProps();
        }
    };

    const handleFontClockColorChange = (event) => {
        setClockColor(event.target.value);
    };

    const handleFontClockColorKeyDown = (event) => {
        if (event.key === 'Enter' || event.key === 'Tab') {
            //alert(TempfontColor.length)
            if (clockColor.length < 1) {
                alert("Font Color can't be empty.");
            } else {
                //setFontColurUI();
                setClockProps();
            }
        }
    };

    const handleFontClockColorBlur = () => {
        if (clockColor.length < 1) {
            alert("Font Color can't be empty.");
        } else {
            //setFontColurUI();
            setClockProps();
        }
    };

    const handleFontFamilyChange = (event) => {
        setTempFontFamily(event.target.value);
    };

    const handleFontFamilyKeyDown = (event) => {
        if (event.key === 'Enter' || event.key === 'Tab') {
            if (TempfontFamily.length < 1) {
                alert("Font Family can't be empty.");
            } else {
                setClockProps();
            }
        }
    };
    const handleFontFamilyBlur = () => {
        if (TempfontFamily.length < 1) {
            alert("Font Family can't be empty.");
        } else {
            setClockProps();
        }
    };

    const handleTitleFontSizeChange = (event) => {
        setTitleFontSize(event.target.value);
        setClockProps();
    };

    const handleClockFontSizeChange = (event) => {
        setClockFontSize(event.target.value);
        setClockProps();
    };

    const handleCreateUpdate = async () => {
        if (selectedPresetId === null) {
            savePreset();
        } else {
            const isCreate = window.confirm('Do you want to update the current selected preset?');
            if (isCreate) {
                updatePreset();
            } 
        }
    }
    const savePreset = async () => {
        //alert(TempfontFamily)
        //alert(fontFamily)
        //alert(titleColors)
        const vFontFamily = TempfontFamily === undefined ? fontFamily : TempfontFamily;
        //alert(vFontFamily)
        const presetData = {
            FontFamily: vFontFamily,
            //FontFamily: fontFamily,
            TitleFontSize: titlefontSize,
            ClockFontSize: clockfontSize,
            TitleFontColor: titleColors[0],
            ClockFontColor: clockColors[0]
        };
        console.log(presetData)
        try {
            const response = await fetch('clock/presets', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(presetData)
            });

            if (response.ok) {
                alert('Preset saved successfully!');
                fetchPresets();
            } else {
                alert('Failed to save preset.');
            }
        } catch (error) {
            console.error('Error saving preset:', error);
            alert('Error connecting to the server.');
        }
    };

    const updatePreset = async () => {
        const vFontFamily = TempfontFamily === undefined ? fontFamily : TempfontFamily;            
        const presetData = {
            ID: selectedPresetId,
            FontFamily: vFontFamily,
            //FontFamily: fontFamily,
            TitleFontSize: titlefontSize,
            ClockFontSize: clockfontSize,
            TitleFontColor: titleColors[0],
            ClockFontColor: clockColors[0]
        };
        console.log(presetData)
        try {
            const response = await fetch('clock/presets', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(presetData)
            });

            if (response.ok) {
                alert('Preset saved successfully!');
                fetchPresets();
            } else {
                alert('Failed to save preset.');
            }
        } catch (error) {
            console.error('Error saving preset:', error);
            alert('Error connecting to the server.');
        }
    }
    const handlePresetSelection = async (presetId) => {
        if (presetId === selectedPresetId) {
            setSelectedPresetId(null);
            setClockPropsFromCheckBox(null);
        } else {
            setSelectedPresetId(presetId);
            try {
                const response = await fetch(`clock/presets/${presetId}`);
                if (response.ok) {
                    const data = await response.json();
                    console.log(data);
                    setClockPropsFromCheckBox(data);
                }                
            } catch (error) {
                console.error('Error loading preset:', error);
            }
        }   
    };

  return (
    <div id="ClockProps">
        <div
            style={{
                float: 'left',
                width: '40px',
                height: '100%',
                border: '1px solid white',
                fontSize: '20pt',
            }}
        >
            <a style={{ cursor: 'pointer' }} onClick={togglePanel}>+/-</a>
        </div>

        {showPanel && (
            <div className="clock-settings-panel">
                <div>
                    <h1>Clock Properties</h1>
                    <hr />
                </div>
                <div>
                    <div>
                        <h2>Settings</h2>
                    </div>
                    <div>
                        <div>Font Family</div>
                        <div>
                            <input
                                id="fontFamily"
                                value={TempfontFamily}
                                onKeyDown={handleFontFamilyKeyDown}
                                onChange={handleFontFamilyChange}
                            />
                            <button onClick={setClockProps}>✓</button>
                        </div>
                    </div>
                    <div>
                        <div>Title Font Size</div>
                        <div>
                            <input id="titleFontSizeSlide"
                                type="range"
                                min="1"        // Minimum font size
                                max="100"        // Maximum font size
                                value={titlefontSize}
                                onChange={handleTitleFontSizeChange}
                                onBlur={handleFontFamilyBlur}
                            />
                        </div>
                    </div>
                    <div>
                        <div>Clock Font Size</div>
                        <div>
                            <input id="clockFontSizeSlide"
                                type="range"
                                min="1"        // Minimum font size
                                max="100"        // Maximum font size
                                value={clockfontSize}
                                onChange={handleClockFontSizeChange}
                            />
                        </div>
                    </div>
                    <div>
                        <div>Title Font Color</div>
                        <div>
                            {/*<button onClick={setClockProps}>✓</button>*/}
                            {titleColors.map((color, index) => (
                                <div key={index}>
                                    <div class="ColorCenteredDiv">
                                        <input
                                            id="titleFontColorPicker"
                                            type="color"
                                            value={color}
                                            onChange={(e) => handleTitleColorChange(index, e.target.value)}
                                        />
                                    </div>
                                    <div>
                                        <input
                                            id="titleFontColor"
                                            value={titleColors}
                                            onKeyDown={handleFontTitleColorKeyDown}
                                            onChange={handleFontTitleColorChange}
                                            onBlur={handleFontTitleColorBlur}
                                            disabled="true"
                                        />
                                    </div>
                                </div>
                            ))}
                        </div>
                    </div>
                    <div>
                        <div>Clock Font Color</div>
                        <div>
                            {clockColors.map((color, index) => (
                                <div key={index}>
                                    <div class="ColorCenteredDiv">
                                        <input
                                            id="clockFontColorPicker"
                                            type="color"
                                            value={color}
                                            onChange={(e) => handlClockColorChange(index, e.target.value)}
                                        />
                                    </div>
                                    <div>
                                        <input
                                            id="clockfontColor"
                                            value={clockColors}
                                            onKeyDown={handleFontClockColorKeyDown}
                                            onChange={handleFontClockColorChange}
                                            onBlur={handleFontClockColorBlur}
                                            disabled="true"
                                        />
                                    </div>
                                </div>
                            ))}
                            {/*<button onClick={setClockProps}>✓</button>*/}
                        </div>
                    </div>
                    <div>
                        <div>Blink Colons</div>
                        <div>
                            <input
                                id="blinkColons"
                                checked={blinkColons}
                                type="checkbox"
                                onChange={setBlinkColonsUI}
                            />
                        </div>
                    </div>
                    <div>
                        <div>
                            <button onClick={handleCreateUpdate} style={{ marginTop: '20px' }}>Save Preset</button>
                        </div>
                    </div>
                </div>
                <hr />
                <div>
                    <h2>Presets</h2>
                    <div>{presetsDisplay}</div>
                </div>
            </div>
        )}
    <div>
      </div>
    </div>
  )
}

export default SetClockProps
