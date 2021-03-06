import request from '@/utils/request'

export function login(data) {
  return request({
    url: '/user/login',
    method: 'post',
    data
  })
}

export function getInfo(token) {
  return request({
    url: `/user/${token}`,
    method: 'get'
  })
}

export function logout() {
  return request({
    url: '/user/logout',
    method: 'post'
  })
}


export function logup(data) {
  return request({
    url: '/user/logup',
    method: 'post',
    data
  })
}

export function searchUser(data) {
  return request({
    url: "/user/search",
    method: "post",
    data
  })
}
