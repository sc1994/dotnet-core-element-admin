import request from '@/utils/request'

export function start() {
  request({
    url: "/stresstest",
    method: "get"
  })
}
