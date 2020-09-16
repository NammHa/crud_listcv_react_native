import React, { Component } from 'react';

import { StyleSheet, View, Alert, TextInput, Button, Text, Platform, TouchableOpacity, ListView, ActivityIndicator } from 'react-native';

import { StackNavigator } from 'react-navigation';

import RNPickerSelect from 'react-native-picker-select';

class MainActivity extends Component {

  static navigationOptions =
    {
      title: 'MainActivity',
    };

  constructor(props) {

    super(props)

    this.state = {

      TextInput_TenCV: '',

      RNPicker_TenHT: '',

      RNPicker_MaHD: '',

      RNPicker_TenChuTri: '',

      DatePicker_NgayBĐ: '',

      DatePicker_NgayKT: '',

      RNPicker_KQ: '',

      TextInput_GhiChu: '',
    }

  }
// ----------------------------------------THÊM MỚI-------------------------------------------------------------
  
InsertStudentRecordsToServer = () => {

    fetch('https://qlcv-api.conveyor.cloud/api/AddNewCV', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({

        TEN_CONG_VIEC: this.state.TextInput_TenCV,

        TEN: this.state.RNPicker_TenHT,

        MA_HOP_DONG: this.state.RNPicker_MaHD,

        FullName: this.state.RNPicker_TenChuTri,

        NGAY_BAT_DAU: this.state.DatePicker_NgayBĐ,

        NGAY_KET_THUC: this.state.DatePicker_NgayKT,

        ID_KET_QUA_CV: this.state.RNPicker_KQ,

        GHI_CHU: this.state.TextInput_GhiChu
      })

    }).then((response) => response.json())
      .then((responseJson) => {

        // Showing response message coming from server after inserting records.
        Alert.alert(responseJson);

      }).catch((error) => {
        console.error(error);
      });

  }

  GoTo_Show_ListCV_Activity_Function = () => {
    this.props.navigation.navigate('Second');
  }

  render() {
    return (

      <View style={styles.MainContainer}>


        <Text style={{ fontSize: 20, textAlign: 'center', marginBottom: 7 }}> Thêm mới công việc </Text>

        <TextInput

          placeholder="Nhập tên công việc"

          onChangeText={TextInputValue => this.setState({ TextInput_TenCV: TextInputValue })}

          underlineColorAndroid='transparent'

          style={styles.TextInputStyleClass}
        />

        <RNPickerSelect onValueChange={(value) => console.log(value)}
          //renderItem = {({item}) => <Text> { `${item.TEN}`} </Text>}
          items={[
            { label: item.TEN, value: item.ID_HE_THONG }
          ]}
          style={styles.TextInputStyleClass}
        >
        </RNPickerSelect>


        <RNPickerSelect onValueChange={(value) => console.log(value)}
          //renderItem = {({item}) => <Text> { `${item.MA_HOP_DONG}`} </Text>}
          items={[
            { label: item.MA_HOP_DONG, value: item.ID_HOP_DONG }
          ]}
          style={styles.TextInputStyleClass}
        >
        </RNPickerSelect>

        <DatePicker
          style={{ width: 200 }}
          date={this.state.DatePicker_NgayBĐ}
          mode="date"
          placeholder="Ngày bắt đầu"
          format="DD-MM-YYYY"
          minDate="01-01-2020"
          maxDate="31-12-2020"
          confirmBtnText="Confirm"
          cancelBtnText="Cancel"
          customStyles={{
            dateIcon: {
              position: 'absolute',
              left: 0,
              top: 4,
              marginLeft: 0
            },
            dateInput: {
              marginLeft: 36
            }
            // ... You can check the source to find the other keys.
          }}
          onDateChange={(date) => { this.setState({ DatePicker_NgayBĐ: date }) }}
        />

        <DatePicker
          style={{ width: 200 }}
          date={this.state.DatePicker_NgayKT}
          mode="date"
          placeholder="Ngày kết thúc"
          format="DD-MM-YYYY"
          minDate="01-01-2020"
          maxDate="31-12-2020"
          confirmBtnText="Confirm"
          cancelBtnText="Cancel"
          customStyles={{
            dateIcon: {
              position: 'absolute',
              left: 0,
              top: 4,
              marginLeft: 0
            },
            dateInput: {
              marginLeft: 36
            }
            // ... You can check the source to find the other keys.
          }}
          onDateChange={(date) => { this.setState({ DatePicker_NgayKT: date }) }}
        />


        <RNPickerSelect onValueChange={(value) => console.log(value)}
          //renderItem = {({item}) => <Text> { `${item.FullName}`} </Text>}
          items={[
            { label: item.FullName, value: item.ID_NGUOI_CHU_TRI }
          ]}
          style={styles.TextInputStyleClass}
        >
        </RNPickerSelect>

        <RNPickerSelect onValueChange={(value) => console.log(value)}
          //renderItem = {({item}) => <Text> { `${item.ID_KET_QUA_CV}`} </Text>}
          items={[
            { label: item.ID_KET_QUA_CV, value: item.ID_KET_QUA_CV }
          ]}
          style={styles.TextInputStyleClass}
        >
        </RNPickerSelect>

        <TextInput

          placeholder="Nhập ghi chú"

          onChangeText={TextInputValue => this.setState({ TextInput_GhiChu: TextInputValue })}

          underlineColorAndroid='transparent'

          style={styles.TextInputStyleClass}
        />

        <TouchableOpacity activeOpacity={.4} style={styles.TouchableOpacityStyle} onPress={this.InsertStudentRecordsToServer} >

          <Text style={styles.TextStyle}> LƯU VÀO HỆ THỐNG </Text>

        </TouchableOpacity>

        <TouchableOpacity activeOpacity={.4} style={styles.TouchableOpacityStyle} onPress={this.GoTo_Show_StudentList_Activity_Function} >

          <Text style={styles.TextStyle}> HỦY </Text>

        </TouchableOpacity>

      </View>

    );
  }
}

// ----------------------------------------HIỂN THỊ DANH SÁCH-------------------------------------------------------------

class HienThiDSCV extends Component {

  constructor(props) {

    super(props);

    this.state = {

      isLoading: true

    }
  }

  static navigationOptions =
    {
      title: 'HIỂN THỊ DANH SÁCH CÔNG VIỆC',
    };

  componentDidMount() {

    return fetch('https://qlcv-api.conveyor.cloud/api/GetListCV')
      .then((response) => response.json())
      .then((responseJson) => {
        let ds = new ListView.DataSource({ rowHasChanged: (r1, r2) => r1 !== r2 });
        this.setState({
          isLoading: false,
          dataSource: ds.cloneWithRows(responseJson),
        }, function () {
          // In this block you can do something with new state.
        });
      })
      .catch((error) => {
        console.error(error);
      });
  }

  GetCVIDFunction = (ID, TEN_CONG_VIEC, TEN, MA_HOP_DONG,  FullName, NGAY_BAT_DAU, NGAY_KET_THUC, ID_KET_QUA_CV, GHI_CHU) => {

    this.props.navigation.navigate('Third', {

      ID: ID,
      TENCV: TEN_CONG_VIEC,
      TENHT: TEN,
      MAHD: MA_HOP_DONG,
      NGUOICT: FullName,
      NGAYBD: NGAY_BAT_DAU,
      NGAYKT: NGAY_KET_THUC,
      KQ: ID_KET_QUA_CV,
      GHICHU: GHI_CHU 
    });

  }

  ListViewItemSeparator = () => {
    return (
      <View
        style={{
          height: .5,
          width: "100%",
          backgroundColor: "#000",
        }}
      />
    );
  }

  render() {
    if (this.state.isLoading) {
      return (
        <View style={{ flex: 1, paddingTop: 20 }}>
          <ActivityIndicator />
        </View>
      );
    }

    return (

      <View style={styles.MainContainer_For_Show_ListCV_Activity}>

        <ListView

          dataSource={this.state.dataSource}

          renderSeparator={this.ListViewItemSeparator}

          renderRow={(rowData) => <Text style={styles.rowViewContainer}

            onPress={this.GetCVIDFunction.bind(
              this, rowData.ID,
              rowData.TEN_CONG_VIEC,
              rowData.TEN,
              rowData.MA_HOP_DONG,
              rowData.FullName,
              rowData.NGAY_BAT_DAU,
              rowData.NGAY_KET_THUC,
              rowData.ID_KET_QUA_CV,
              rowData.GHI_CHU
            )} >

            {rowData.TEN_CONG_VIEC}

          </Text>}

        />

      </View>
    );
  }

}

// ----------------------------------------CHỈNH SỬA-------------------------------------------------------------

class ChinhSuaDSCV extends Component {

  constructor(props) {

    super(props)

    this.state = {

      TextInput_TenCV: '',

      RNPicker_TenHT: '',

      RNPicker_MaHD: '',

      RNPicker_TenChuTri: '',

      DatePicker_NgayBĐ: '',

      DatePicker_NgayKT: '',

      RNPicker_KQ: '',

      TextInput_GhiChu: '',

    }

  }

  componentDidMount() {

    // Received Student Details Sent From Previous Activity and Set Into State.
    this.setState({
      TextInput_TenCV: this.props.navigation.state.params.TENCV,
      RNPicker_TenHT: this.props.navigation.state.params.TENHT,
      RNPicker_MaHD: this.props.navigation.state.params.MAHD,
      RNPicker_TenChuTri: this.props.navigation.state.params.NGUOICT,
      DatePicker_NgayBĐ: this.props.navigation.state.params.NGAYBD,
      DatePicker_NgayKT: this.props.navigation.state.params.NGAYKT,
      RNPicker_KQ: this.props.navigation.state.params.KQ,
      TextInput_GhiChu: this.props.navigation.state.params.GHICHU,
    })

  }

  static navigationOptions =
    {
      title: 'CHỈNH SỬA DANH SÁCH CÔNG VIỆC',
    };

  UpdateDSCV = () => {

    fetch('https://qlcv-api.conveyor.cloud/api/EditCV', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({

        TEN_CONG_VIEC: this.state.TextInput_TenCV,

        TEN: this.state.RNPicker_TenHT,

        MA_HOP_DONG: this.state.RNPicker_MaHD,

        FullName: this.state.RNPicker_TenChuTri,

        NGAY_BAT_DAU: this.state.DatePicker_NgayBĐ,

        NGAY_KET_THUC: this.state.DatePicker_NgayKT,

        ID_KET_QUA_CV: this.state.RNPicker_KQ,

        GHI_CHU: this.state.TextInput_GhiChu
      })

    }).then((response) => response.json())
      .then((responseJson) => {

        // Showing response message coming from server updating records.
        Alert.alert(responseJson);

      }).catch((error) => {
        console.error(error);
      });

  }

// ----------------------------------------XÓA-------------------------------------------------------------

  /*DeleteStudentRecord = () => {

    fetch('https://reactnativecode.000webhostapp.com/Student/DeleteStudentRecord.php', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({

        student_id: this.state.TextInput_Student_ID

      })

    }).then((response) => response.json())
      .then((responseJson) => {

        // Showing response message coming from server after inserting records.
        Alert.alert(responseJson);

      }).catch((error) => {
        console.error(error);
      });

    this.props.navigation.navigate('First');

  }

  render() {

    return (

      <View style={styles.MainContainer}>

        <Text style={{ fontSize: 20, textAlign: 'center', marginBottom: 7 }}> Edit Student Record Form </Text>

        <TextInput

          placeholder="Student Name Shows Here"

          value={this.state.TextInput_Student_Name}

          onChangeText={TextInputValue => this.setState({ TextInput_Student_Name: TextInputValue })}

          underlineColorAndroid='transparent'

          style={styles.TextInputStyleClass}
        />

        <TextInput

          placeholder="Student Class Shows Here"

          value={this.state.TextInput_Student_Class}

          onChangeText={TextInputValue => this.setState({ TextInput_Student_Class: TextInputValue })}

          underlineColorAndroid='transparent'

          style={styles.TextInputStyleClass}
        />

        <TextInput

          placeholder="Student Phone Number Shows Here"

          value={this.state.TextInput_Student_PhoneNumber}

          onChangeText={TextInputValue => this.setState({ TextInput_Student_PhoneNumber: TextInputValue })}

          underlineColorAndroid='transparent'

          style={styles.TextInputStyleClass}
        />

        <TextInput

          placeholder="Student Email Shows Here"

          value={this.state.TextInput_Student_Email}

          onChangeText={TextInputValue => this.setState({ TextInput_Student_Email: TextInputValue })}

          underlineColorAndroid='transparent'

          style={styles.TextInputStyleClass}
        />

        <TouchableOpacity activeOpacity={.4} style={styles.TouchableOpacityStyle} onPress={this.UpdateStudentRecord} >

          <Text style={styles.TextStyle}> UPDATE STUDENT RECORD </Text>

        </TouchableOpacity>

        <TouchableOpacity activeOpacity={.4} style={styles.TouchableOpacityStyle} onPress={this.DeleteStudentRecord} >

          <Text style={styles.TextStyle}> DELETE CURRENT RECORD </Text>

        </TouchableOpacity>


      </View>

    );
  }*/

}

export default MyNewProject = StackNavigator(

  {

    First: { screen: MainActivity },

    Second: { screen: HienThiDSCV },

    Third: { screen: ChinhSuaDSCV }

  });

const styles = StyleSheet.create({

  MainContainer: {

    alignItems: 'center',
    flex: 1,
    paddingTop: 30,
    backgroundColor: '#fff'

  },

  MainContainer_For_Show_StudentList_Activity: {

    flex: 1,
    paddingTop: (Platform.OS == 'ios') ? 20 : 0,
    marginLeft: 5,
    marginRight: 5

  },

  TextInputStyleClass: {

    textAlign: 'center',
    width: '90%',
    marginBottom: 7,
    height: 40,
    borderWidth: 1,
    borderColor: '#FF5722',
    borderRadius: 5,

  },

  TouchableOpacityStyle: {

    paddingTop: 10,
    paddingBottom: 10,
    borderRadius: 5,
    marginBottom: 7,
    width: '90%',
    backgroundColor: '#00BCD4'

  },

  TextStyle: {
    color: '#fff',
    textAlign: 'center',
  },

  rowViewContainer: {
    fontSize: 20,
    paddingRight: 10,
    paddingTop: 10,
    paddingBottom: 10,
  }

});