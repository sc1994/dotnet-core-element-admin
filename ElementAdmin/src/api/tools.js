import request from '@/utils/request'

export function initRoutesData(data) {
  return request({
    url: '/tools/initroutedata',
    method: 'post',
    data
  })
}

export function initEntities(data) {
  return request({
    url: '/tools/initentities',
    method: 'post',
    data
  })
}

export function getEntities() {
  return request({
    url: "/tools/getentities",
    method: "get"
  })
}
