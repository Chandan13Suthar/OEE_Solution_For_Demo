var dom = document.getElementById('container');                                                         // Get the DOM element with the id "container"

 setInterval(function () { myChart.setOption(option); }, 10000);
var myChart = echarts.init(dom, null,
    {                                                                 // Initialize an ECharts chart
      renderer: 'canvas',                                                                                   // Use the canvas renderer
      useDirtyRect: false                                                                                   // Disable dirty rect optimization
    });
    var app = {};                                                                                           // Create an empty JavaScript object
    
    var option;                                                                                             // Declare a variable named "option" for chart configuration

    option = {                                                                                              // Define the chart configuration options
            tooltip: {                                                                                      // Configure the tooltip
                trigger: 'axis',                                                                            // Trigger the tooltip on axes
                axisPointer: {                                                                              // Configure the axis pointer for the tooltip
                    type: 'shadow'                                                                          // Use 'shadow' type for the axis pointer; it can also be 'line' or 'shadow'
                }
            },

  legend: {},                                                                                               // Configure the legend (usually for chart series names)
  grid: {                                                                                                   // Configure the grid layout
    left: '3%',                                                                                             // Left margin
    right: '4%',                                                                                            // Right margin
    bottom: '3%',                                                                                           // Bottom margin
    containLabel: true                                                                                      // Allow labels to be contained within the grid
  },
  xAxis: {                                                                                                  // Configure the X-axis
    type: 'value'                                                                                           // Use a numeric X-axis
  },
  yAxis: {                                                                                                  // Configure the Y-axis
    type: 'category',                                                                                        // Use a category (text) Y-axis
    data: ['MACHINE 01', 'MACHINE 02', 'MACHINE 03', 'MACHINE 04', 'MACHINE 05', 'MACHINE 06', 'MACHINE 07'] // Specify Y-axis data labels
  },
        series: [                                                                                             // Configure chart series data
            {
                name: 'Production Count',                                                                             // Series name
                type: 'bar',                                                                                          // Bar chart type
                stack: 'total',
                Refresh: 'Auto',                                                                                               // Stack bars in the 'total' group
                label: {                                                                                              // Configure labels for bars
                    show: true                                                                                          // Show labels
                },
                emphasis: {                                                                                           // Configure emphasis settings
                    focus: 'series'                                                                                     // Emphasize the entire series on interaction
                },
                data: [$01, 0, 0, $01, 0, 0, 0]
                
           //data: [1320, 2302, 1301, 1234, 1390, 1330, 2720] // Series data values

             // value: [{eChart1, eChart2, eChart3, eChart4, eChart5, eChart6, eChart7}]

          },

          {
      name: 'Availablity', 
      type: 'bar',
      stack: 'total',
      label: {
        show: true
      },
      emphasis: {
        focus: 'series'
      },
    /*  data: [
                  {
                      value: [$01, $02, $03, $04, $05, $06, $07],
                  },
              ],*/
      
              data: [$02, 0, 0, $02, 0, 0, 0]
    },
    {
      name: 'Quality',
      type: 'bar',
      stack: 'total',
      label: {
        show: true
      },
      emphasis: {
        focus: 'series'
      },


       /* data: [
            {
                value: [$01, $02, $03, $04, $05, $06, $07],
            },
        ],*/

        data: [$03, 0, 0, 0, 0, 0, 0]
    },
    {
      name: 'Performance',
      type: 'bar',
      stack: 'total',
      label: {
        show: true
      },
      emphasis: {
        focus: 'series'
      },

        /*data: [
            {
                value: [$01, $02, $03, $04, $05, $06, $07],
            },
        ],*/

        data: [$04, 0, 0, 0, 0, 0, 0]
    },
    {
      name: 'OEE',
      type: 'bar',
      stack: 'total',
      label: {
        show: true
      },
      emphasis: {
        focus: 'series'
      },

        /*data: [
            {
                value: [$01, $02, $03, $04, $05, $06, $07],
            },
        ],*/

        data: [$05, 0, 0, 0, 0, 0, 0]
    }                                                                                                          // Similar configurations for other series...
  ]
};
    if (option && typeof option === 'object') {                                                             // Check if the "option" object is valid
      myChart.setOption(option);                                                                            // Apply the chart configuration options
    }

   window.addEventListener('resize', myChart.resize);                                                      // Add a resize event listener to handle chart resizing

